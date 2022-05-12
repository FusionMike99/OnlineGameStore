using System.Linq;
using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineGameStore.BLL.Entities;
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
        public void Create_Get_ReturnsViewResult(
            GameController sut)
        {
            // Act
            var result = sut.Create();

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<EditGameViewModel>();
        }

        [Theory]
        [AutoMoqData]
        public void Create_Post_ReturnsRedirectToActionResult_WhenGameIsValid(
            Game game,
            [Frozen] Mock<IGameService> mockGameService,
            GameController sut)
        {
            // Arrange
            mockGameService.Setup(x => x.CreateGame(It.IsAny<Game>()))
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
            var result = sut.Create(editGameViewModel);

            // Assert
            result.Should().BeOfType<RedirectToActionResult>()
                .Subject.ActionName.Should().BeEquivalentTo(nameof(sut.GetGames));

            mockGameService.Verify(x => x.CreateGame(It.IsAny<Game>()), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public void Create_Post_ReturnsViewResult_WhenGameIsInvalid(
            EditGameViewModel editGameViewModel,
            GameController sut)
        {
            // Arrange
            sut.ModelState.AddModelError("Name", "Required");

            // Act
            var result = sut.Create(editGameViewModel);

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<EditGameViewModel>()
                .Which.Id.Should().Be(editGameViewModel.Id);
        }
    }
}