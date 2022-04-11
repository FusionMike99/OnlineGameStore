using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Services.Contracts;
using OnlineGameStore.MVC.Controllers;
using OnlineGameStore.MVC.Models;
using OnlineGameStore.Tests.Helpers;
using System.Linq;
using Xunit;

namespace OnlineGameStore.Tests.Controllers
{
    public partial class GameControllerTests
    {
        [Theory]
        [AutoMoqData]
        public void Update_ReturnsJsonResult_WhenGameIsValid(
            Game game,
            [Frozen] Mock<IGameService> mockGameService,
            GameController sut)
        {
            // Arrange
            mockGameService.Setup(x => x.EditGame(It.IsAny<Game>()))
                .Returns(game);

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
            var result = sut.Update(editGameViewModel);

            // Assert
            result.Should().BeOfType<JsonResult>()
                .Which.Value.Should().BeAssignableTo<GameViewModel>()
                .Which.Name.Should().Be(editGameViewModel.Name);

            mockGameService.Verify(x => x.EditGame(It.IsAny<Game>()), Times.Once);
        }

        [Theory]
        [InlineAutoMoqData(null)]
        public void Update_ReturnsBadRequestObjectResult_WhenGameIsInvalid(
            EditGameViewModel editGameViewModel,
            GameController sut)
        {
            // Arrange
            sut.ModelState.AddModelError("Name", "Required");

            // Act
            var result = sut.Update(editGameViewModel);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>()
                .Which.Value.Should().BeOfType<SerializableError>();
        }
    }
}
