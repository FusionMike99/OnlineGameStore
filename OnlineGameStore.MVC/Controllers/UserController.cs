﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineGameStore.BLL.Models.General;
using OnlineGameStore.BLL.Services.Interfaces;
using OnlineGameStore.DomainModels.Constants;
using OnlineGameStore.DomainModels.Enums;
using OnlineGameStore.DomainModels.Models.General;
using OnlineGameStore.Identity.Services.Interfaces;
using OnlineGameStore.MVC.Infrastructure;
using OnlineGameStore.MVC.Models;

namespace OnlineGameStore.MVC.Controllers
{
    [Route("users")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IPublisherService _publisherService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IRoleService roleService,
            IPublisherService publisherService, IMapper mapper)
        {
            _userService = userService;
            _roleService = roleService;
            _publisherService = publisherService;
            _mapper = mapper;
        }
        
        [HttpGet("update/{userName}")]
        [AuthorizeByRoles(Permissions.UserPermission)]
        public async Task<IActionResult> Update([FromRoute] string userName, [FromQuery] string returnUrl = default)
        {
            if (!User.IsInRole(Roles.Admin) && User.Identity.Name != userName)
            {
                return Forbid();
            }
            
            var user = await _userService.GetUserByNameAsync(userName);
            if (user == null)
            {
                return NotFound();
            }

            var editUserViewModel = _mapper.Map<EditUserViewModel>(user);
            await ConfigureEditUserViewModel(editUserViewModel);
            
            return View(editUserViewModel);
        }

        [HttpPost("update/{userName}")]
        [AuthorizeByRoles(Permissions.UserPermission)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromRoute] string userName, [FromForm] EditUserViewModel user,
            [FromQuery] string returnUrl = default)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            var mappedUser = _mapper.Map<UserModel>(user);
            var isEditOwnProfile = userName == User.Identity.Name;
            await _userService.EditUserAsync(userName, mappedUser, isEditOwnProfile);

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("GetGames", "Game");
        }
        
        [HttpGet]
        [AuthorizeByRoles(Permissions.AdminPermission)]
        public async Task<IActionResult> GetUsers()
        {
            var user = await _userService.GetAllUsersAsync();
            var usersViewModel = _mapper.Map<IEnumerable<UserViewModel>>(user);

            return View("Index", usersViewModel);
        }
        
        [HttpPost("remove")]
        [AuthorizeByRoles(Permissions.AdminPermission)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove([FromForm] string userName)
        {
            await _userService.DeleteUserAsync(userName);

            return RedirectToAction(nameof(GetUsers));
        }

        [HttpGet("{userName}/ban")]
        [AuthorizeByRoles(Permissions.ModeratorPermission)]
        public IActionResult Ban(string userName, string returnUrl = null)
        {
            var banViewModel = new BanViewModel
            {
                UserName = userName,
                ReturnUrl = returnUrl
            };
            
            return View(banViewModel);
        }

        [HttpPost("{userName}/ban")]
        [ValidateAntiForgeryToken]
        [AuthorizeByRoles(Permissions.ModeratorPermission)]
        public IActionResult Ban(BanViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            var message = _userService.BanUser(model.UserName, model.BanPeriod!.Value);

            if (string.IsNullOrWhiteSpace(model.ReturnUrl) || !Url.IsLocalUrl(model.ReturnUrl))
            {
                return RedirectToAction(nameof(GameController.GetGames), "Game");
            }
            
            var afterBanViewModel = new AfterBanViewModel
            {
                Message = message,
                ReturnUrl = model.ReturnUrl
            };
                
            return View("AfterBan", afterBanViewModel);
        }

        private async Task ConfigureEditUserViewModel(EditUserViewModel model)
        {
            var roles = await _roleService.GetAllRolesAsync();
            model.Roles = new SelectList(roles, nameof(RoleModel.Name), nameof(RoleModel.Name));

            var publishers = await _publisherService.GetAllPublishersAsync();
            model.Publishers =
                new SelectList(publishers, nameof(PublisherModel.Id), nameof(PublisherModel.CompanyName));
        }
    }
}