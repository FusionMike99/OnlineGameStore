using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using OnlineGameStore.BLL.Models.General;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Services;
using OnlineGameStore.Tests.Helpers;
using Xunit;

namespace OnlineGameStore.Tests.Services
{
    public partial class GameServiceTests
    {
        [Theory]
        [AutoMoqData]
        public async Task GameService_GetGameByKey_ReturnsGame_WhenGameFromGameStore(
            GameModel game,
            [Frozen] Mock<IGameRepository> gameRepositoryMock,
            GameService sut)
        {
            // Arrange
            gameRepositoryMock.Setup(x => x.GetByKeyAsync(It.IsAny<string>(),
                    It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(game);

            // Act
            var actualGame = await sut.GetGameByKeyAsync(game.Key, increaseViews: true);

            // Assert
            actualGame.Should().Be(game);

            gameRepositoryMock.Verify(x => x.GetByKeyAsync(It.IsAny<string>(),
                    It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);
        }
    }
}