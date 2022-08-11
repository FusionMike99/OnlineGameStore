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
        public async Task GameService_EditGame_ReturnsGame(
            GameModel game,
            [Frozen] Mock<IGameRepository> gameRepositoryMock,
            GameService sut)
        {
            // Arrange
            gameRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<GameModel>()));

            // Act
            var actualGame = await sut.EditGame(game);

            // Assert
            actualGame.Should().BeEquivalentTo(game);

            gameRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<GameModel>()), Times.Once);
        }
    }
}