using OnlineGameStore.DomainModels.Enums;

namespace OnlineGameStore.BLL.Services.Interfaces
{
    public interface IUserService
    {
        string BanUser(string userName, BanPeriod banPeriod);
    }
}