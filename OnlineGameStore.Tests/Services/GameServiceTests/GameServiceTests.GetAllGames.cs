using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using OnlineGameStore.BLL.Models;
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
        public async Task GameService_GetAllGames_ReturnsGames(
            List<GameModel> games,
            SortFilterGameModel sortFilterGameModel,
            [Frozen] Mock<IGameRepository> gameRepositoryMock,
            GameService sut)
        {
            // Arrange
            var expectedCount = games.Count;
            gameRepositoryMock.Setup(x => x.GetAllAsync(It.IsAny<SortFilterGameModel>(),
                    It.IsAny<PageModel>()))
                .ReturnsAsync((games, expectedCount));

            // Act
            var actualGames = await sut.GetAllGames(sortFilterGameModel);

            // Assert
            actualGames.Item1.Should().BeEquivalentTo(games);
            actualGames.Item2.Should().Be(expectedCount);

            gameRepositoryMock.Verify(x => x.GetAllAsync(It.IsAny<SortFilterGameModel>(),
                    It.IsAny<PageModel>()), Times.Once);
        }
    }
}