using OnlineGameStore.BLL.Enums;

namespace OnlineGameStore.BLL.Services.Contracts
{
    public interface IUserService
    {
        string BanUser(string userName, BanPeriod banPeriod);
    }
}