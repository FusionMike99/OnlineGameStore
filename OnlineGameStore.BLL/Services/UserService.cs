using Microsoft.Extensions.Logging;
using OnlineGameStore.BLL.Enums;
using OnlineGameStore.BLL.Services.Contracts;

namespace OnlineGameStore.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;

        public UserService(ILogger<UserService> logger)
        {
            _logger = logger;
        }
        
        public string BanUser(string userName, BanPeriod banPeriod)
        {
            var message = $"User {userName} is banned for a {banPeriod}";
            
            _logger.LogDebug($@"Class: {nameof(UserService)}; Method: {nameof(BanUser)}.
                    Banning user with user name {userName} with period {banPeriod} successfully",
                userName, banPeriod);

            return message;
        }
    }
}