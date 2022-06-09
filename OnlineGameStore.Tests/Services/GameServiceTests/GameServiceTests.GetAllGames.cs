using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Models;
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
        public void GameService_GetAllGames_ReturnsGames(
            IEnumerable<Game> games,
            Genre genre,
            SortFilterGameModel sortFilterGameModel,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            GameService sut)
        {
            // Arrange
            mockUnitOfWork.Setup(m => m.Games.GetMany(It.IsAny<Expression<Func<Game, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<Func<IQueryable<Game>,IOrderedQueryable<Game>>>(),
                    It.IsAny<int?>(),
                    It.IsAny<int?>(),
                    It.IsAny<string[]>()))
                .Returns(games);

            mockUnitOfWork.Setup(m => m.Genres.GetSingle(It.IsAny<Expression<Func<Genre, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<string[]>()))
                .Returns(genre);

            // Act
            var actualGames = sut.GetAllGames(sortFilterGameModel);

            // Assert
            actualGames.Should().BeEquivalentTo(games);

            mockUnitOfWork.Verify(x => x.Games.GetMany(
                    It.IsAny<Expression<Func<Game, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<Func<IQueryable<Game>,IOrderedQueryable<Game>>>(),
                    It.IsAny<int?>(),
                    It.IsAny<int?>(),
                    It.IsAny<string[]>()),
                Times.Once);
        }
    }
}