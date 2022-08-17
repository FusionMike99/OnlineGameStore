using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineGameStore.BLL.Models.General;
using OnlineGameStore.Identity.Services.Interfaces;
using OnlineGameStore.MVC.Models;

namespace OnlineGameStore.MVC.Controllers
{
    [Route("users")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IRoleService roleService, IMapper mapper)
        {
            _userService = userService;
            _roleService = roleService;
            _mapper = mapper;
        }
        
        [HttpGet("update/{userName}")]
        public async Task<IActionResult> Update([FromRoute] string userName)
        {
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(string userName, [FromForm] EditUserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            var mappedUser = _mapper.Map<UserModel>(user);
            await _userService.EditUserAsync(mappedUser);

            return RedirectToAction(nameof(GetUsers));
        }
        
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var user = await _userService.GetAllUsersAsync();
            var usersViewModel = _mapper.Map<IEnumerable<UserViewModel>>(user);

            return View("Index", usersViewModel);
        }
        
        [HttpPost("remove")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove([FromForm] string userName)
        {
            await _userService.DeleteUserAsync(userName);

            return RedirectToAction(nameof(GetUsers));
        }

        [HttpGet("{userName}/ban")]
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
        }
    }
}