using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
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
        public async Task GameService_GetGamesNumber_ReturnsGamesNumber(
            int gamesNumber,
            SortFilterGameModel sortFilterGameModel,
            [Frozen] Mock<IGameRepository> gameRepositoryMock,
            GameService sut)
        {
            // Arrange
            gameRepositoryMock.Setup(x => x.GetGamesNumberAsync(It.IsAny<SortFilterGameModel>()))
                .ReturnsAsync(gamesNumber);

            // Act
            var actualGamesNumber = await sut.GetGamesNumberAsync(sortFilterGameModel);

            // Assert
            actualGamesNumber.Should().Be(gamesNumber);

            gameRepositoryMock.Verify(x => x.GetGamesNumberAsync(It.IsAny<SortFilterGameModel>()),
                Times.Once);
        }
    }
}