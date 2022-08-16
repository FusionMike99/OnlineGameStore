using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using OnlineGameStore.BLL.Exceptions;
using OnlineGameStore.DAL.Entities;
using OnlineGameStore.Identity.Models;
using OnlineGameStore.Identity.Services.Interfaces;

namespace OnlineGameStore.Identity.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly SignInManager<UserEntity> _signInManager;

        public AuthenticationService(SignInManager<UserEntity> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<bool> LogInAsync(LoginModel loginModel)
        {
            var result = await _signInManager.PasswordSignInAsync(loginModel.Email, loginModel.Password,
                loginModel.RememberMe, lockoutOnFailure: false);

            if (result.IsLockedOut)
            {
                throw new UserException("This account is locked out");
            }

            return result.Succeeded;
        }

        public async Task LogOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}