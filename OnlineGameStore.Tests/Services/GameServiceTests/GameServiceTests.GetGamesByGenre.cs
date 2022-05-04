using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Services;
using OnlineGameStore.Tests.Helpers;
using System;
using System.Collections.Generic;
using Xunit;

namespace OnlineGameStore.Tests.Services
{
    public partial class GameServiceTests
    {
        [Theory]
        [AutoMoqData]
        public void GameService_GetGamesByGenre_ReturnsGames(
            int genreId,
            IEnumerable<Game> games,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            GameService sut)
        {
            // Arrange
            mockUnitOfWork
                .Setup(m => m.Games.GetMany(
                    It.IsAny<Func<Game, bool>>(),
                    It.IsAny<bool>(),
                    It.IsAny<string[]>()))
                .Returns(games);

            // Act
            var actualGames = sut.GetGamesByGenre(genreId);

            // Assert
            actualGames.Should().BeEquivalentTo(games);

            mockUnitOfWork.Verify(x => x.Games.GetMany(
                It.IsAny<Func<Game, bool>>(),
                It.IsAny<bool>(),
                It.IsAny<string[]>()),
                Times.Once);
        }
    }
}
