using System.Threading.Tasks;
using OnlineGameStore.Identity.Models;

namespace OnlineGameStore.Identity.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task<bool> LogInAsync(LoginModel loginModel);

        Task LogOutAsync();
    }
}