using System.Threading.Tasks;
using OnlineGameStore.Identity.Models;

namespace OnlineGameStore.Identity.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<bool> LogInAsync(LoginModel loginModel);

        Task LogOutAsync();
    }
}