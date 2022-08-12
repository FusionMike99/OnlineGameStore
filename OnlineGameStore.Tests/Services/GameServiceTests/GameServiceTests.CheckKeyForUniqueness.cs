using System;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using OnlineGameStore.BLL.Models.General;
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
        public async Task CheckKeyForUniqueness_ReturnsTrue_WhenGameIsNotNullAndIdIsNotSame(
            GameModel game,
            Guid id,
            [Frozen] Mock<IGameRepository> gameRepositoryMock,
            GameService sut)
        {
            // Arrange
            gameRepositoryMock.Setup(x => x.GetByKeyAsync(It.IsAny<string>(),
                    It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(game);

            // Act
            var actualResult = await sut.CheckKeyForUniqueAsync(id, game.Key);

            // Assert
            actualResult.Should().BeTrue();

            gameRepositoryMock.Verify(x => x.GetByKeyAsync(It.IsAny<string>(),
                    It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);
        }

        [Theory]
        [InlineAutoMoqData(null)]
        public async Task CheckKeyForUniqueness_ReturnsFalse_WhenGameIsNull(
            GameModel game,
            Guid id,
            string gameKey,
            [Frozen] Mock<IGameRepository> gameRepositoryMock,
            GameService sut)
        {
            // Arrange
            gameRepositoryMock.Setup(x => x.GetByKeyAsync(It.IsAny<string>(),
                    It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(game);

            // Act
            var actualResult = await sut.CheckKeyForUniqueAsync(id, gameKey);

            // Assert
            actualResult.Should().BeFalse();

            gameRepositoryMock.Verify(x => x.GetByKeyAsync(It.IsAny<string>(),
                    It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public async Task CheckKeyForUniqueness_ReturnsFalse_WhenIdIsSame(
            GameModel game,
            [Frozen] Mock<IGameRepository> gameRepositoryMock,
            GameService sut)
        {
            // Arrange
            gameRepositoryMock.Setup(x => x.GetByKeyAsync(It.IsAny<string>(),
                    It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(game);

            // Act
            var actualResult = await sut.CheckKeyForUniqueAsync(game.Id, game.Key);

            // Assert
            actualResult.Should().BeFalse();

            gameRepositoryMock.Verify(x => x.GetByKeyAsync(It.IsAny<string>(),
                    It.IsAny<bool>(), It.IsAny<bool>()), Times.Once);
        }
    }
}