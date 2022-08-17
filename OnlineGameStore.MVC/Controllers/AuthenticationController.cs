using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineGameStore.BLL.Exceptions;
using OnlineGameStore.Identity.Models;
using OnlineGameStore.Identity.Services.Interfaces;
using OnlineGameStore.MVC.Models;

namespace OnlineGameStore.MVC.Controllers
{
    [Route("auth")]
    public class AuthenticationController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;

        public AuthenticationController(IUserService userService, IAuthenticationService authenticationService,
            IMapper mapper)
        {
            _userService = userService;
            _authenticationService = authenticationService;
            _mapper = mapper;
        }
        
        [HttpGet("register")]
        public IActionResult Register(string returnUrl = null)
        {
            var model = new RegisterViewModel
            {
                ReturnUrl = returnUrl
            };
            
            return View(model);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var registerModel = _mapper.Map<RegisterModel>(model);
            try
            {
                await _userService.CreateUserAsync(registerModel);
                
                return SetUpReturnUrl(model.ReturnUrl);
            }
            catch (UserException userException)
            {
                foreach (var error in userException.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }

            return View("Register", model);
        }
        
        [HttpGet("login")]
        public IActionResult LogIn(string returnUrl = null)
        {
            var model = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };
            
            return View(model);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LogIn(LoginViewModel model)
        {
            var loginModel = _mapper.Map<LoginModel>(model);
            try
            {
                var result = await _authenticationService.LogInAsync(loginModel);

                if (!result)
                {
                    ModelState.AddModelError("", "Email or password are wrong");
                    return View(model);
                }
                
                return SetUpReturnUrl(model.ReturnUrl);
            }
            catch (UserException userException)
            {
                foreach (var error in userException.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }

            return View("LogIn", model);
        }

        [HttpGet("logout")]
        public async Task<IActionResult> LogOut()
        {
            await _authenticationService.LogOutAsync();
            
            return RedirectToAction("GetGames", "Game");
        }

        [HttpGet("access-denied")]
        public IActionResult AccessDenied() => View();

        private IActionResult SetUpReturnUrl(string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("GetGames", "Game");
        }
    }
}