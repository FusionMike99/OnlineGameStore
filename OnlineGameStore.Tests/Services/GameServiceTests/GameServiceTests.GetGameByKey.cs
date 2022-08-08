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
        [AutoMoqData]
        public void GameService_GetGameByKey_ReturnsGame_WhenGameFromGameStore(
            Game game,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            GameService sut)
        {
            // Arrange
            game.DatabaseEntity = DatabaseEntity.GameStore;
            
            mockUnitOfWork.Setup(m => m.Games.GetSingle(
                    It.IsAny<Expression<Func<Game, bool>>>(),
                    It.IsAny<bool>(), It.IsAny<string[]>()))
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

            mockUnitOfWork.Verify(x => x.Games.Update(It.IsAny<Game>(),
                    It.IsAny<Expression<Func<Game, bool>>>()),
                Times.Once);
            
            mockUnitOfWork.Verify(x => x.Commit(), Times.Once);
        }
        
        [Theory]
        [InlineAutoMoqData(null)]
        public void GameService_GetGameByKey_ReturnsGame_WhenGameFromNorthwind(
            Game game,
            NorthwindProduct product,
            NorthwindSupplier supplier,
            NorthwindCategory category,
            Genre genre,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            [Frozen] Mock<INorthwindUnitOfWork> mockNorthwindUnitOfWork,
            GameService sut)
        {
            // Arrange
            mockUnitOfWork.Setup(m => m.Games.GetSingle(
                    It.IsAny<Expression<Func<Game, bool>>>(),
                    It.IsAny<bool>(), It.IsAny<string[]>()))
                .Returns(game);

            mockNorthwindUnitOfWork.Setup(m => m.Products.GetFirst(
                    It.IsAny<Expression<Func<NorthwindProduct, bool>>>()))
                .Returns(product);
            
            mockNorthwindUnitOfWork.Setup(m => m.Suppliers.GetFirst(
                    It.IsAny<Expression<Func<NorthwindSupplier, bool>>>()))
                .Returns(supplier);
            
            mockNorthwindUnitOfWork.Setup(m => m.Categories.GetFirst(
                    It.IsAny<Expression<Func<NorthwindCategory, bool>>>()))
                .Returns(category);

            mockUnitOfWork.Setup(m => m.Genres.GetSingle(It.IsAny<Expression<Func<Genre, bool>>>(),
                    It.IsAny<bool>(), It.IsAny<string[]>()))
                .Returns(genre);

            var expectedViewsNumber = product.ViewsNumber + 1;

            // Act
            var actualGame = sut.GetGameByKey(product.Key, increaseViews: true);

            // Assert
            actualGame.Id.Should().Be(product.ProductId);
            actualGame.ViewsNumber.Should().Be(expectedViewsNumber);

            mockUnitOfWork.Verify(x => x.Games.GetSingle(
                    It.IsAny<Expression<Func<Game, bool>>>(),
                    It.IsAny<bool>(), It.IsAny<string[]>()),
                Times.Once);
            
            mockNorthwindUnitOfWork.Verify(m => m.Products.GetFirst(
                    It.IsAny<Expression<Func<NorthwindProduct, bool>>>()),
                Times.Once);
            
            mockNorthwindUnitOfWork.Verify(m => m.Suppliers.GetFirst(
                    It.IsAny<Expression<Func<NorthwindSupplier, bool>>>()),
                Times.Once);
            
            mockNorthwindUnitOfWork.Verify(m => m.Categories.GetFirst(
                    It.IsAny<Expression<Func<NorthwindCategory, bool>>>()),
                Times.Once);

            mockUnitOfWork.Verify(m => m.Genres.GetSingle(It.IsAny<Expression<Func<Genre, bool>>>(),
                    It.IsAny<bool>(), It.IsAny<string[]>()),
                Times.Once);
            
            mockNorthwindUnitOfWork.Verify(m => m.Products.Update(
                    It.IsAny<Expression<Func<NorthwindProduct,bool>>>(),
                    It.IsAny<NorthwindProduct>()),
                Times.Once);
        }
    }
}