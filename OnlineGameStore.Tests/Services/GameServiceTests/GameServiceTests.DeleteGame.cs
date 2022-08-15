using System;
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
        public async Task GameService_DeleteGame_DeletesGame(
            GameModel game,
            [Frozen] Mock<IGameRepository> gameRepositoryMock,
            GameService sut)
        {
            // Arrange
            gameRepositoryMock.Setup(x => x.GetByKeyAsync(It.IsAny<string>(),
                    It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(game);

            gameRepositoryMock.Setup(x => x.DeleteOrCreateAsync(It.IsAny<GameModel>()));

            // Act
            await sut.DeleteGameAsync(game.Key);

            // Assert
            gameRepositoryMock.Verify(x => x.GetByKeyAsync(It.IsAny<string>(),
                    It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);
            gameRepositoryMock.Verify(x => x.DeleteOrCreateAsync(It.IsAny<GameModel>()), Times.Once);
        }

        [Theory]
        [InlineAutoMoqData(null, null)]
        public async Task GameService_DeleteGame_ThrowsInvalidOperationExceptionWithNullEntity(
            GameModel game,
            string gameKey,
            [Frozen] Mock<IGameRepository> gameRepositoryMock,
            GameService sut)
        {
            // Arrange
            gameRepositoryMock.Setup(x => x.GetByKeyAsync(It.IsAny<string>(),
                    It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(game);

            // Act
            Func<Task> actual = async () => await sut.DeleteGameAsync(gameKey);

            // Assert
            await actual.Should().ThrowAsync<InvalidOperationException>();

            gameRepositoryMock.Verify(x => x.GetByKeyAsync(It.IsAny<string>(),
                It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);
        }
    }
}