﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineGameStore.BLL.Models.General;
using OnlineGameStore.DAL.Entities;
using OnlineGameStore.Identity.Services.Interfaces;

namespace OnlineGameStore.Identity.Services
{
    public class RoleService : IRoleService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly IMapper _mapper;

        public RoleService(UserManager<UserEntity> userManager, RoleManager<IdentityRole<Guid>> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task AttachRoleToUserAsync(string userName, string roleName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return;
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            if (userRoles.FirstOrDefault() == roleName)
            {
                return;
            }
            
            await _userManager.RemoveFromRolesAsync(user, userRoles);
            await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task<IEnumerable<RoleModel>> GetAllRolesAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            var mappedRoles = _mapper.Map<IEnumerable<RoleModel>>(roles);

            return mappedRoles;
        }
    }
}