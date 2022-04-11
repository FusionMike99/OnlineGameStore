using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Services;
using OnlineGameStore.Tests.Helpers;
using System;
using System.Linq.Expressions;
using Xunit;

namespace OnlineGameStore.Tests.Services
{
    public partial class GameServiceTests
    {
        [Theory]
        [AutoMoqData]
        public void GameService_DeleteGame_DeletesGame(
            int gameId,
            Game game,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            GameService sut)
        {
            // Arrange
            mockUnitOfWork
                .Setup(m => m.Games.GetSingle(It.IsAny<Expression<Func<Game, bool>>>()))
                .Returns(game);

            mockUnitOfWork.Setup(x => x.Games.Delete(It.IsAny<Game>()));

            // Act
            sut.DeleteGame(gameId);

            // Assert
            mockUnitOfWork.Verify(x => x.Games.GetSingle(
                It.IsAny<Expression<Func<Game, bool>>>()),
                Times.Once);
            mockUnitOfWork.Verify(x => x.Games.Delete(
                It.Is<Game>(g => g.Name == game.Name && g.Id == game.Id)),
                Times.Once);
            mockUnitOfWork.Verify(x => x.Commit(), Times.Once);
        }

        [Theory]
        [InlineAutoMoqData(null)]
        public void GameService_DeleteGame_ThrowsInvalidOperationExceptionWithNullEntity(
            Game game,
            int gameId,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            GameService sut)
        {
            // Arrange
            mockUnitOfWork
                .Setup(m => m.Games.GetSingle(It.IsAny<Expression<Func<Game, bool>>>()))
                .Returns(game);

            mockUnitOfWork.Setup(x => x.Games.Delete(It.IsAny<Game>()));

            // Act
            Action actual = () => sut.DeleteGame(gameId);

            // Assert
            actual.Should().Throw<InvalidOperationException>();

            mockUnitOfWork.Verify(x => x.Games.GetSingle(
                It.IsAny<Expression<Func<Game, bool>>>(),
                It.IsAny<string[]>()),
                Times.Once);
        }
    }
}
