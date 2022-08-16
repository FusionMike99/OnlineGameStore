using Microsoft.AspNetCore.Mvc;
using OnlineGameStore.BLL.Services.Interfaces;
using OnlineGameStore.MVC.Models;

namespace OnlineGameStore.MVC.Controllers
{
    [Route("users")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{userName}/ban")]
        public IActionResult Ban(string userName, string returnUrl = "")
        {
            var banViewModel = new BanViewModel
            {
                UserName = userName,
                ReturnUrl = returnUrl
            };
            
            return View(banViewModel);
        }

        [HttpPost("{userName}/ban")]
        public IActionResult Ban(BanViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            var message = _userService.BanUser(model.UserName, model.BanPeriod.Value);

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
    }
}