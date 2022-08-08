using System;
using System.Linq.Expressions;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Entities.Northwind;
using OnlineGameStore.BLL.Enums;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Repositories.GameStore;
using OnlineGameStore.BLL.Repositories.Northwind;
using OnlineGameStore.BLL.Services;
using OnlineGameStore.Tests.Helpers;
using Xunit;

namespace OnlineGameStore.Tests.Services
{
    public partial class GameServiceTests
    {
        [Theory]
        [InlineAutoMoqData(DatabaseEntity.GameStore)]
        public void GameService_UpdateGameQuantity_WhenGameFromGameStore(
            DatabaseEntity databaseEntity,
            Game game,
            short quantity,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            GameService sut)
        {
            // Arrange
            game.DatabaseEntity = databaseEntity;
            
            mockUnitOfWork
                .Setup(m => m.Games.GetSingle(It.IsAny<Expression<Func<Game, bool>>>(),
                    It.IsAny<bool>(), It.IsAny<string[]>()))
                .Returns(game);

            short Operation(short a, short b) => (short)(a + b);

            var expectedUnits = (short)(game.UnitsInStock + quantity);

            // Act
            sut.UpdateGameQuantity(game.Key, quantity, Operation);

            // Assert
            game.UnitsInStock.Should().Be(expectedUnits);
            
            mockUnitOfWork.Verify(x => x.Games.GetSingle(
                    It.IsAny<Expression<Func<Game, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<string[]>()),
                Times.Once);
            
            mockUnitOfWork.Verify(x => x.Games.Update(It.IsAny<Game>(),
                    It.IsAny<Expression<Func<Game, bool>>>()),
                Times.Once);
        }
        
        [Theory]
        [InlineAutoMoqData(DatabaseEntity.Northwind)]
        public void GameService_UpdateGameQuantity_WhenGameFromNorthwind(
            DatabaseEntity databaseEntity,
            Game game,
            short quantity,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            [Frozen] Mock<INorthwindUnitOfWork> mockNorthwindUnitOfWork,
            GameService sut)
        {
            // Arrange
            game.DatabaseEntity = databaseEntity;
            
            mockUnitOfWork
                .Setup(m => m.Games.GetSingle(It.IsAny<Expression<Func<Game, bool>>>(),
                    It.IsAny<bool>(), It.IsAny<string[]>()))
                .Returns(game);

            short Operation(short a, short b) => (short)(a + b);

            var expectedUnits = (short)(game.UnitsInStock + quantity);

            // Act
            sut.UpdateGameQuantity(game.Key, quantity, Operation);

            // Assert
            game.UnitsInStock.Should().Be(expectedUnits);
            
            mockUnitOfWork.Verify(x => x.Games.GetSingle(
                    It.IsAny<Expression<Func<Game, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<string[]>()),
                Times.Once);

            mockNorthwindUnitOfWork.Verify(x => x.Products.Update(
                    It.IsAny<Expression<Func<NorthwindProduct, bool>>>(),
                    It.IsAny<NorthwindProduct>()),
                Times.Once);
        }
    }
}