using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Enums;
using OnlineGameStore.BLL.Models.General;

namespace OnlineGameStore.Identity.Interfaces.Services
{
    public interface IUserService
    {
        Task<bool> CreateUserAsync(UserModel user, string password);

        Task<UserModel> EditUserAsync(UserModel user);

        Task DeleteUserAsync(string userName);

        Task<UserModel> GetUserByNameAsync(string userName);

        Task<IEnumerable<UserModel>> GetAllUsersAsync();

        string BanUser(string userName, BanPeriod banPeriod);
    }
}