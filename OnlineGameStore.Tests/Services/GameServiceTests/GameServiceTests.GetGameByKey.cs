using System;
using System.Linq.Expressions;
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
        public void GameService_GetGameByKey_ReturnsGame(
            Game game,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            GameService sut)
        {
            // Arrange
            mockUnitOfWork
                .Setup(m => m.Games.GetSingle(
                    It.IsAny<Expression<Func<Game, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<string[]>()))
                .Returns(game);

            var expectedViewsNumber = game.ViewsNumber + 1;

            // Act
            var actualGame = sut.GetGameByKey(game.Key, increaseViews: true);

            // Assert
            actualGame.ViewsNumber.Should().Be(expectedViewsNumber);

            mockUnitOfWork.Verify(x => x.Games.GetSingle(
                    It.IsAny<Expression<Func<Game, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<string[]>()),
                Times.Once);
            
            mockUnitOfWork.Verify(x => x.Commit(), Times.Once);
        }
    }
}