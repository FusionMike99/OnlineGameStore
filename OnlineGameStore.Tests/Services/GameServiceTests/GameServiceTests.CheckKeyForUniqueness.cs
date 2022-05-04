using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Services;
using OnlineGameStore.Tests.Helpers;
using System;
using Xunit;

namespace OnlineGameStore.Tests.Services
{
    public partial class GameServiceTests
    {
        [Theory]
        [AutoMoqData]
        public void CheckKeyForUniqueness_ReturnsTrue_WhenGameIsNotNullAndIdIsNotSame(
            Game game,
            int id,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            GameService sut)
        {
            // Arrange
            mockUnitOfWork
                .Setup(m => m.Games.GetSingle(
                    It.IsAny<Func<Game, bool>>(),
                    It.IsAny<bool>(),
                    It.IsAny<string[]>()))
                .Returns(game);

            // Act
            var actualResult = sut.CheckKeyForUniqueness(id, game.Key);

            // Assert
            actualResult.Should().BeTrue();

            mockUnitOfWork.Verify(x => x.Games.GetSingle(
                It.IsAny<Func<Game, bool>>(),
                It.IsAny<bool>(),
                It.IsAny<string[]>()),
                Times.Once);
        }

        [Theory]
        [InlineAutoMoqData(null)]
        public void CheckKeyForUniqueness_ReturnsFalse_WhenGameIsNull(
            Game game,
            int id,
            string gameKey,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            GameService sut)
        {
            // Arrange
            mockUnitOfWork
                .Setup(m => m.Games.GetSingle(
                    It.IsAny<Func<Game, bool>>(),
                    It.IsAny<bool>(),
                    It.IsAny<string[]>()))
                .Returns(game);

            // Act
            var actualResult = sut.CheckKeyForUniqueness(id, gameKey);

            // Assert
            actualResult.Should().BeFalse();

            mockUnitOfWork.Verify(x => x.Games.GetSingle(
                It.IsAny<Func<Game, bool>>(),
                It.IsAny<bool>(),
                It.IsAny<string[]>()),
                Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public void CheckKeyForUniqueness_ReturnsFalse_WhenIdIsSame(
            Game game,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            GameService sut)
        {
            // Arrange
            mockUnitOfWork
                .Setup(m => m.Games.GetSingle(
                    It.IsAny<Func<Game, bool>>(),
                    It.IsAny<bool>(),
                    It.IsAny<string[]>()))
                .Returns(game);

            // Act
            var actualResult = sut.CheckKeyForUniqueness(game.Id, game.Key);

            // Assert
            actualResult.Should().BeFalse();

            mockUnitOfWork.Verify(x => x.Games.GetSingle(
                It.IsAny<Func<Game, bool>>(),
                It.IsAny<bool>(),
                It.IsAny<string[]>()),
                Times.Once);
        }
    }
}
