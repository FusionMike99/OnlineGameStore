using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using OnlineGameStore.BLL.Entities;
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
        public void GameService_EditGame_ReturnsGame(
            Game game,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            GameService sut)
        {
            // Arrange
            mockUnitOfWork.Setup(x => x.Games.Update(It.IsAny<Game>()))
                .Returns(game);

            // Act
            var actualGame = sut.EditGame(game);

            // Assert
            actualGame.Should().BeEquivalentTo(game);

            mockUnitOfWork.Verify(x => x.Games.Update(It.IsAny<Game>()), Times.Once);
            mockUnitOfWork.Verify(x => x.Commit(), Times.Once);
        }
    }
}