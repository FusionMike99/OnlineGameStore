using System.Threading.Tasks;

namespace OnlineGameStore.Identity.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task<bool> LogInAsync(string email, string password, bool isPersistent);

        Task LogOutAsync();
    }
}