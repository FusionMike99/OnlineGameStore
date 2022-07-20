using System;
using System.Linq.Expressions;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Entities.Northwind;
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
        public void GameService_DeleteGame_DeletesGame_WhenGameFromGameStore(
            DatabaseEntity databaseEntity,
            Game game,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            GameService sut)
        {
            // Arrange
            game.DatabaseEntity = databaseEntity;
            
            mockUnitOfWork
                .Setup(m => m.Games.GetSingle(
                    It.IsAny<Expression<Func<Game, bool>>>(),
                    It.IsAny<bool>(), It.IsAny<string[]>()))
                .Returns(game);

            mockUnitOfWork.Setup(x => x.Games.Delete(It.IsAny<Game>()));

            // Act
            sut.DeleteGame(game.Key);

            // Assert
            mockUnitOfWork.Verify(x => x.Games.GetSingle(
                    It.IsAny<Expression<Func<Game, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<string[]>()),
                Times.Once);
            mockUnitOfWork.Verify(x => x.Games.Delete(
                    It.Is<Game>(g => g.Name == game.Name && g.Id == game.Id)),
                Times.Once);
            mockUnitOfWork.Verify(x => x.Commit(), Times.Once);
        }
        
        [Theory]
        [InlineAutoMoqData(DatabaseEntity.Northwind)]
        public void GameService_DeleteGame_DeletesGame_WhenGameFromNorthwind(
            DatabaseEntity databaseEntity,
            Game game,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            GameService sut)
        {
            // Arrange
            game.DatabaseEntity = databaseEntity;
            
            mockUnitOfWork.Setup(m => m.Games.GetSingle(
                    It.IsAny<Expression<Func<Game, bool>>>(),
                    It.IsAny<bool>(), It.IsAny<string[]>()))
                .Returns(game);
            
            mockUnitOfWork.Setup(x => x.Games.Create(It.IsAny<Game>()))
                .Returns(game);

            // Act
            sut.DeleteGame(game.Key);

            // Assert
            mockUnitOfWork.Verify(x => x.Games.Create(It.IsAny<Game>()),
                Times.Once);
            mockUnitOfWork.Verify(x => x.Commit(), Times.Once);
        }

        [Theory]
        [InlineAutoMoqData(null, null)]
        public void GameService_DeleteGame_ThrowsInvalidOperationExceptionWithNullEntity(
            Game game,
            NorthwindProduct product,
            string gameKey,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            [Frozen] Mock<INorthwindUnitOfWork> mockNorthwindUnitOfWork,
            GameService sut)
        {
            // Arrange
            mockUnitOfWork.Setup(m => m.Games.GetSingle(
                    It.IsAny<Expression<Func<Game, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<string[]>()))
                .Returns(game);

            mockNorthwindUnitOfWork.Setup(m => m.Products.GetFirst(
                    It.IsAny<Expression<Func<NorthwindProduct, bool>>>()))
                .Returns(product);

            // Act
            Action actual = () => sut.DeleteGame(gameKey);

            // Assert
            actual.Should().Throw<InvalidOperationException>();

            mockUnitOfWork.Verify(x => x.Games.GetSingle(
                    It.IsAny<Expression<Func<Game, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<string[]>()),
                Times.Once);
            
            mockNorthwindUnitOfWork.Verify(x => x.Products.GetFirst(
                It.IsAny<Expression<Func<NorthwindProduct,bool>>>()),
                Times.Once);
        }
    }
}