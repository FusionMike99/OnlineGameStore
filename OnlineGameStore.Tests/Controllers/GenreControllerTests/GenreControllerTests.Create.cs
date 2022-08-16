using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineGameStore.BLL.Services.Interfaces;
using OnlineGameStore.DomainModels.Models.General;
using OnlineGameStore.MVC.Controllers;
using OnlineGameStore.MVC.Models;
using OnlineGameStore.Tests.Helpers;
using Xunit;

namespace OnlineGameStore.Tests.Controllers
{
    public partial class GenreControllerTests
    {
        [Theory]
        [AutoMoqData]
        public async Task Create_Get_ReturnsViewResult(
            GenreController sut)
        {
            // Act
            var result = await sut.Create();

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<EditGenreViewModel>();
        }

        [Theory]
        [AutoMoqData]
        public async Task Create_Post_ReturnsRedirectToActionResult_WhenGenreIsValid(
            GenreModel genre,
            [Frozen] Mock<IGenreService> mockGenreService,
            GenreController sut)
        {
            // Arrange
            mockGenreService.Setup(x => x.CreateGenreAsync(It.IsAny<GenreModel>()))
                .ReturnsAsync(genre);

            var editGenreViewModel = new EditGenreViewModel
            {
                Id = genre.Id,
                Name = genre.Name,
                SelectedParentGenre = genre.ParentId
            };

            // Act
            var result = await sut.Create(editGenreViewModel);

            // Assert
            result.Should().BeOfType<RedirectToActionResult>()
                .Subject.ActionName.Should().BeEquivalentTo(nameof(sut.GetGenres));

            mockGenreService.Verify(x => x.CreateGenreAsync(It.IsAny<GenreModel>()), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public async Task Create_Post_ReturnsViewResult_WhenGenreIsInvalid(
            EditGenreViewModel editGenreViewModel,
            GenreController sut)
        {
            // Arrange
            sut.ModelState.AddModelError("Name", "Required");

            // Act
            var result = await sut.Create(editGenreViewModel);

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<EditGenreViewModel>()
                .Which.Id.Should().Be(editGenreViewModel.Id);
        }
    }
}