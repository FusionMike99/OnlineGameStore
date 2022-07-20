using System;
using System.Linq.Expressions;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Enums;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Services;
using OnlineGameStore.Tests.Helpers;
using Xunit;

namespace OnlineGameStore.Tests.Services
{
    public partial class GameServiceTests
    {
        [Theory]
        [InlineAutoMoqData(DatabaseEntity.GameStore)]
        public void GameService_EditGame_ReturnsGame_WhenGameFromGameStore(
            DatabaseEntity databaseEntity,
            Game game,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            GameService sut)
        {
            // Arrange
            game.DatabaseEntity = databaseEntity;
            
            mockUnitOfWork.Setup(x => x.Games.Update(It.IsAny<Game>(),
                    It.IsAny<Expression<Func<Game,bool>>>()))
                .Returns(game);

            // Act
            var actualGame = sut.EditGame(game.Key, game);

            // Assert
            actualGame.Should().BeEquivalentTo(game);

            mockUnitOfWork.Verify(x => x.Games.Update(It.IsAny<Game>(),
                It.IsAny<Expression<Func<Game,bool>>>()), Times.Once);
            mockUnitOfWork.Verify(x => x.Commit(), Times.Once);
        }
        
        [Theory]
        [InlineAutoMoqData(DatabaseEntity.Northwind)]
        public void GameService_EditGame_ReturnsGame_WhenGameFromNorthwind(
            DatabaseEntity databaseEntity,
            Game game,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            GameService sut)
        {
            // Arrange
            game.DatabaseEntity = databaseEntity;
            
            mockUnitOfWork.Setup(x => x.Games.Create(It.IsAny<Game>()))
                .Returns(game);

            // Act
            var actualGame = sut.EditGame(game.Key, game);

            // Assert
            actualGame.Should().BeEquivalentTo(game);

            mockUnitOfWork.Verify(x => x.Games.Create(It.IsAny<Game>()),
                Times.Once);
            mockUnitOfWork.Verify(x => x.Commit(), Times.Once);
        }
    }
}