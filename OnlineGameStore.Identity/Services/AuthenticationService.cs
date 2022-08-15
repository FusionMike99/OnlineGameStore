using System.Threading.Tasks;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.Identity.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using OnlineGameStore.BLL.Exceptions;

namespace OnlineGameStore.Identity.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly SignInManager<UserEntity> _signInManager;

        public AuthenticationService(SignInManager<UserEntity> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<bool> LogInAsync(string email, string password, bool isPersistent)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, isPersistent, false);

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