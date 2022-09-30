using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineGameStore.BLL.Exceptions;
using OnlineGameStore.BLL.Models.General;
using OnlineGameStore.BLL.Services.Interfaces;
using OnlineGameStore.DAL.Entities;
using OnlineGameStore.DomainModels.Constants;
using OnlineGameStore.DomainModels.Enums;
using OnlineGameStore.Identity.Models;
using OnlineGameStore.Identity.Services.Interfaces;

namespace OnlineGameStore.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly IPublisherService _publisherService;
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public UserService(ILogger<UserService> logger, UserManager<UserEntity> userManager,
            SignInManager<UserEntity> signInManager, IPublisherService publisherService,
            IRoleService roleService, IMapper mapper)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _publisherService = publisherService;
            _roleService = roleService;
            _mapper = mapper;
        }

        public async Task CreateUserAsync(RegisterModel registerModel)
        {
            var mappedUser = _mapper.Map<UserEntity>(registerModel);
            var result = await _userManager.CreateAsync(mappedUser, registerModel.Password);
            ValidateUser(result);
            await _roleService.AttachRoleToUserAsync(mappedUser.UserName, Roles.User.ToString());
        }

        public async Task<UserModel> EditUserAsync(string userName, UserModel userModel, bool isOwnProfile = false)
        {
            var currentUser = await _userManager.FindByNameAsync(userName);
            currentUser.Email = userModel.Email;
            currentUser.UserName = userModel.UserName;
            currentUser.PublisherId = userModel.PublisherId;
            
            var result = await _userManager.UpdateAsync(currentUser);
            ValidateUser(result);

            if (string.IsNullOrWhiteSpace(userModel.Role))
            {
                return userModel;
            }
            
            await _roleService.AttachRoleToUserAsync(currentUser.UserName, userModel.Role);
            
            if(userModel.Role == Roles.Publisher.ToString())
            {
                await SetPublisherClaim(currentUser, userModel.PublisherId);
            }
            else
            {
                await RemovePublisherClaim(currentUser);
            }

            if (isOwnProfile)
            {
                await _signInManager.RefreshSignInAsync(currentUser);
            }

            return userModel;
        }

        public async Task DeleteUserAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return;
            }

            // Need to check. Maybe not working without method SetLockoutEndDateAsync
            await _userManager.SetLockoutEnabledAsync(user, enabled: true);
        }

        public async Task<UserModel> GetUserByNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var mappedUser = _mapper.Map<UserModel>(user);
            mappedUser.Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

            return mappedUser;
        }

        public async Task<IEnumerable<UserModel>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var mappedUsers = _mapper.Map<List<UserModel>>(users);

            return mappedUsers;
        }

        public string BanUser(string userName, BanPeriod banPeriod)
        {
            var message = $"User {userName} is banned for a {banPeriod}";
            
            _logger.LogDebug(@"Service: {Service}; Method: {Method}.
                    Banning user with user name {UserName} with period {BanPeriod} successfully",
                nameof(UserService), nameof(BanUser), userName, banPeriod);

            return message;
        }

        private static void ValidateUser(IdentityResult result)
        {
            if (result.Succeeded)
            {
                return;
            }
            
            var errors = result.Errors.Select(e => e.Description).ToArray();
            throw new UserException(errors);
        }

        private async Task SetPublisherClaim(UserEntity user, Guid? publisherId)
        {
            if(!publisherId.HasValue)
            {
                return;
            }
            
            var publisher = await _publisherService.GetPublisherByIdAsync(publisherId.Value);
            if (publisher == null)
            {
                return;
            }

            var newClaim = new Claim(Claims.Publisher, publisher.CompanyName);
            var oldClaim = (await _userManager.GetClaimsAsync(user))
                .FirstOrDefault(c => c.Type == Claims.Publisher);
            if (oldClaim == null)
            {
                await _userManager.AddClaimAsync(user, newClaim);
            }
            else if (oldClaim.Value != publisher.CompanyName)
            {
                await _userManager.ReplaceClaimAsync(user, oldClaim, newClaim);
            }
        }

        private async Task RemovePublisherClaim(UserEntity user)
        {
            var publisherClaim = (await _userManager.GetClaimsAsync(user))
                .FirstOrDefault(c => c.Type == Claims.Publisher);
            if (publisherClaim == null)
            {
                return;
            }

            await _userManager.RemoveClaimAsync(user, publisherClaim);
        }
    }
}