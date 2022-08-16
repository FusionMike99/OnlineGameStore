using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineGameStore.BLL.Services.Interfaces;
using OnlineGameStore.DomainModels.Models;
using OnlineGameStore.DomainModels.Models.General;
using OnlineGameStore.MVC.Controllers;
using OnlineGameStore.MVC.Models;
using OnlineGameStore.Tests.Helpers;
using Xunit;

namespace OnlineGameStore.Tests.Controllers
{
    public partial class GameControllerTests
    {
        [Theory]
        [AutoMoqData]
        public async Task GetGames_ReturnsViewResult(
            List<GameModel> games,
            SortFilterGameViewModel sortFilterGameViewModel,
            [Frozen] Mock<IGameService> mockGameService,
            GameController sut)
        {
            // Arrange
            var expectedCount = games.Count;
            mockGameService.Setup(x => x.GetAllGamesAsync(It.IsAny<SortFilterGameModel>(),
                    It.IsAny<PageModel>()))
                .ReturnsAsync((games, expectedCount));

            // Act
            var result = await sut.GetGames(sortFilterGameViewModel);

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<GameListViewModel>()
                .Which.Games.Should().HaveSameCount(games);

            mockGameService.Verify(x => x.GetAllGamesAsync(It.IsAny<SortFilterGameModel>(),
                    It.IsAny<PageModel>()),
                Times.Once);
        }
    }
}