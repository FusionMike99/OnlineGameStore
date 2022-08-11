using System.Linq;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineGameStore.BLL.Models.General;
using OnlineGameStore.BLL.Services.Contracts;
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
        public async Task Update_Get_ReturnsViewResult(
            GameModel game,
            [Frozen] Mock<IGameService> mockGameService,
            GameController sut)
        {
            // Arrange
            mockGameService.Setup(x => x.GetGameByKey(It.IsAny<string>(),
                    It.IsAny<bool>()))
                .ReturnsAsync(game);

            // Act
            var result = await sut.Update(game.Key);

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<EditGameViewModel>()
                .Which.Id.Should().Be(game.Id);
            
            mockGameService.Verify(x => x.GetGameByKey(It.IsAny<string>(), It.IsAny<bool>()), Times.Once);
        }

        [Theory]
        [InlineAutoMoqData("")]
        [InlineAutoMoqData(" ")]
        [InlineAutoMoqData(null)]
        public async Task Update_Get_ReturnsBadRequestResult_WhenGameKeyHasNotValue(
            string gameKey,
            GameController sut)
        {
            // Act
            var result = await sut.Update(gameKey);

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Theory]
        [InlineAutoMoqData(null)]
        public async Task Update_Get_ReturnsNotFoundResult_WhenGameIsNotFound(
            GameModel game,
            string gameKey,
            [Frozen] Mock<IGameService> mockGameService,
            GameController sut)
        {
            // Arrange
            mockGameService.Setup(x => x.GetGameByKey(It.IsAny<string>(),
                    It.IsAny<bool>()))
                .ReturnsAsync(game);

            // Act
            var result = await sut.Update(gameKey);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            mockGameService.Verify(x => x.GetGameByKey(It.IsAny<string>(), It.IsAny<bool>()), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public async Task Update_Post_ReturnsRedirectToActionResult_WhenGameIsValid(
            GameModel game,
            [Frozen] Mock<IGameService> mockGameService,
            GameController sut)
        {
            // Arrange
            mockGameService.Setup(x => x.EditGame(It.IsAny<GameModel>()))
                .ReturnsAsync(game);

            var editGameViewModel = new EditGameViewModel
            {
                Id = game.Id,
                Key = game.Key,
                Name = game.Name,
                Description = game.Description,
                SelectedGenres = game.GameGenres.Select(g => g.GenreId).ToList(),
                SelectedPlatformTypes = game.GamePlatformTypes.Select(p => p.PlatformId).ToList()
            };

            // Act
            var result = await sut.Update(editGameViewModel.Key, editGameViewModel);

            // Assert
            result.Should().BeOfType<RedirectToActionResult>()
                .Subject.ActionName.Should().BeEquivalentTo(nameof(sut.GetGames));

            mockGameService.Verify(x => x.EditGame(It.IsAny<GameModel>()), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public async Task Update_Post_ReturnsViewResult_WhenGameIsInvalid(
            EditGameViewModel editGameViewModel,
            GameController sut)
        {
            // Arrange
            sut.ModelState.AddModelError("Name", "Required");

            // Act
            var result = await sut.Update(editGameViewModel.Key, editGameViewModel);

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<EditGameViewModel>()
                .Which.Id.Should().Be(editGameViewModel.Id);
        }
    }
}