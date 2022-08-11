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
            
            _logger.LogDebug(@"Service: {Service}; Method: {Method}.
                    Banning user with user name {UserName} with period {BanPeriod} successfully",
                nameof(UserService), nameof(BanUser), userName, banPeriod);

            return message;
        }
    }
}