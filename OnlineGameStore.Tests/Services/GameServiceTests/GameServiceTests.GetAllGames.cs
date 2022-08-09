using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Entities.Northwind;
using OnlineGameStore.BLL.Models;
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
        public void GameService_GetAllGames_ReturnsGames(
            List<Game> games,
            List<NorthwindProduct> products,
            Genre genre,
            SortFilterGameModel sortFilterGameModel,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            [Frozen] Mock<INorthwindUnitOfWork> mockNorthwindUnitOfWork,
            GameService sut)
        {
            // Arrange
            mockUnitOfWork.Setup(m => m.Games.GetMany(It.IsAny<Expression<Func<Game, bool>>>(),
                    It.IsAny<bool>(), It.IsAny<Func<IQueryable<Game>,IOrderedQueryable<Game>>>(),
                    It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<string[]>()))
                .Returns(games);

            mockNorthwindUnitOfWork.Setup(m => m.Products.GetMany(
                It.IsAny<Expression<Func<NorthwindProduct,bool>>>(),
                It.IsAny<Func<IQueryable<NorthwindProduct>,IOrderedQueryable<NorthwindProduct>>>(),
                It.IsAny<int?>(), It.IsAny<int?>())).Returns(products);

            mockUnitOfWork.Setup(m => m.Genres.GetSingle(It.IsAny<Expression<Func<Genre, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<string[]>()))
                .Returns(genre);

            var expectedCount = games.Count(g => !g.IsDeleted) + products.Count;
            var expectedGenresCount = sortFilterGameModel.SelectedGenres.Count;

            // Act
            var actualGames = sut.GetAllGames(sortFilterGameModel);

            // Assert
            actualGames.Should().HaveCount(expectedCount);

            mockUnitOfWork.Verify(x => x.Games.GetMany(
                    It.IsAny<Expression<Func<Game, bool>>>(),
                    It.IsAny<bool>(), It.IsAny<Func<IQueryable<Game>,IOrderedQueryable<Game>>>(),
                    It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<string[]>()),
                Times.Once);

            mockNorthwindUnitOfWork.Verify(x => x.Products.GetMany(
                    It.IsAny<Expression<Func<NorthwindProduct, bool>>>(),
                    It.IsAny<Func<IQueryable<NorthwindProduct>, IOrderedQueryable<NorthwindProduct>>>(),
                    It.IsAny<int?>(), It.IsAny<int?>()),
                Times.Once);

            mockUnitOfWork.Verify(m => m.Genres.GetSingle(It.IsAny<Expression<Func<Genre, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<string[]>()),
                Times.Exactly(expectedGenresCount));
        }
        
        [Theory]
        [AutoMoqData]
        public void GameService_GetAllGames_WithGamesNumber_ReturnsGames(
            List<Game> games,
            List<NorthwindProduct> products,
            Genre genre,
            SortFilterGameModel sortFilterGameModel,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            [Frozen] Mock<INorthwindUnitOfWork> mockNorthwindUnitOfWork,
            GameService sut)
        {
            // Arrange
            mockUnitOfWork.Setup(m => m.Games.GetMany(It.IsAny<Expression<Func<Game, bool>>>(),
                    It.IsAny<bool>(), It.IsAny<Func<IQueryable<Game>,IOrderedQueryable<Game>>>(),
                    It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<string[]>()))
                .Returns(games);

            mockNorthwindUnitOfWork.Setup(m => m.Products.GetMany(
                It.IsAny<Expression<Func<NorthwindProduct,bool>>>(),
                It.IsAny<Func<IQueryable<NorthwindProduct>,IOrderedQueryable<NorthwindProduct>>>(),
                It.IsAny<int?>(), It.IsAny<int?>())).Returns(products);

            mockUnitOfWork.Setup(m => m.Genres.GetSingle(It.IsAny<Expression<Func<Genre, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<string[]>()))
                .Returns(genre);

            var expectedCount = games.Count(g => !g.IsDeleted) + products.Count;
            var expectedGenresCount = sortFilterGameModel.SelectedGenres.Count;

            // Act
            var actualGames = sut.GetAllGamesWithNumber(out var gamesNumber, sortFilterGameModel);

            // Assert
            actualGames.Should().HaveCount(expectedCount);
            gamesNumber.Should().Be(expectedCount);

            mockUnitOfWork.Verify(x => x.Games.GetMany(
                    It.IsAny<Expression<Func<Game, bool>>>(),
                    It.IsAny<bool>(), It.IsAny<Func<IQueryable<Game>,IOrderedQueryable<Game>>>(),
                    It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<string[]>()),
                Times.Once);

            mockNorthwindUnitOfWork.Verify(x => x.Products.GetMany(
                    It.IsAny<Expression<Func<NorthwindProduct, bool>>>(),
                    It.IsAny<Func<IQueryable<NorthwindProduct>, IOrderedQueryable<NorthwindProduct>>>(),
                    It.IsAny<int?>(), It.IsAny<int?>()),
                Times.Once);

            mockUnitOfWork.Verify(m => m.Genres.GetSingle(It.IsAny<Expression<Func<Genre, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<string[]>()),
                Times.Exactly(expectedGenresCount));
        }
    }
}