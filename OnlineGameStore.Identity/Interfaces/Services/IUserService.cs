using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Enums;
using OnlineGameStore.BLL.Models.General;
using OnlineGameStore.Identity.Models;

namespace OnlineGameStore.Identity.Interfaces.Services
{
    public interface IUserService
    {
        Task<bool> CreateUserAsync(RegisterModel registerModel);

        Task<UserModel> EditUserAsync(UserModel user);

        Task DeleteUserAsync(string userName);

        Task<UserModel> GetUserByNameAsync(string userName);

        Task<IEnumerable<UserModel>> GetAllUsersAsync();

        string BanUser(string userName, BanPeriod banPeriod);
    }
}