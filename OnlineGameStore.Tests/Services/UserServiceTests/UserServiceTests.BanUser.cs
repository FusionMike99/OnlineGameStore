using FluentAssertions;
using OnlineGameStore.BLL.Enums;
using OnlineGameStore.BLL.Services;
using OnlineGameStore.Tests.Helpers;
using Xunit;

namespace OnlineGameStore.Tests.Services.UserServiceTests
{
    public class UserServiceTests
    {
        [Theory]
        [AutoMoqData]
        public void UserService_BanUser_ReturnsString(
            string userName,
            BanPeriod banPeriod,
            UserService sut)
        {
            // Arrange
            var expected = $"User {userName} is banned for a {banPeriod}";
            
            // Act
            var result = sut.BanUser(userName, banPeriod);
            
            // Assert
            result.Should().Be(expected);
        }
    }
}