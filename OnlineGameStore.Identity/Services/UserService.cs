﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Enums;
using OnlineGameStore.BLL.Exceptions;
using OnlineGameStore.BLL.Models.General;
using OnlineGameStore.Identity.Interfaces.Services;

namespace OnlineGameStore.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly UserManager<UserEntity> _userManager;
        private readonly IMapper _mapper;

        public UserService(ILogger<UserService> logger, UserManager<UserEntity> userManager, IMapper mapper)
        {
            _logger = logger;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<bool> CreateUserAsync(UserModel user, string password)
        {
            var mappedUser = _mapper.Map<UserEntity>(user);
            var result = await _userManager.CreateAsync(mappedUser, password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToArray();

                throw new UserException(errors);
            }

            return result.Succeeded;
        }

        public async Task<UserModel> EditUserAsync(UserModel user)
        {
            var mappedUser = _mapper.Map<UserEntity>(user);
            await _userManager.UpdateAsync(mappedUser);

            return user;
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

            return mappedUser;
        }

        public async Task<IEnumerable<UserModel>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var mappedUsers = _mapper.Map<IEnumerable<UserModel>>(users);

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
    }
}