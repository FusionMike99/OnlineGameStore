using System;
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
    public partial class GenreControllerTests
    {
        [Theory]
        [AutoMoqData]
        public async Task Update_Get_ReturnsViewResult(
            GenreModel genre,
            [Frozen] Mock<IGenreService> mockGenreService,
            GenreController sut)
        {
            // Arrange
            mockGenreService.Setup(x => x.GetGenreByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(genre);

            // Act
            var result = await sut.Update(genre.Id);

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<EditGenreViewModel>()
                .Which.Id.Should().Be(genre.Id);
            
            mockGenreService.Verify(x => x.GetGenreByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Theory]
        [InlineAutoMoqData(null)]
        public async Task Update_Get_ReturnsNotFoundResult_WhenGenreIsNotFound(
            GenreModel genre,
            Guid genreId,
            [Frozen] Mock<IGenreService> mockGenreService,
            GenreController sut)
        {
            // Arrange
            mockGenreService.Setup(x => x.GetGenreByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(genre);

            // Act
            var result = await sut.Update(genreId);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            mockGenreService.Verify(x => x.GetGenreByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public async Task Update_Post_ReturnsRedirectToActionResult_WhenGenreIsValid(
            GenreModel genre,
            [Frozen] Mock<IGenreService> mockGenreService,
            GenreController sut)
        {
            // Arrange
            mockGenreService.Setup(x => x.EditGenreAsync(It.IsAny<GenreModel>()))
                .ReturnsAsync(genre);

            var editGenreViewModel = new EditGenreViewModel
            {
                Id = genre.Id,
                Name = genre.Name,
                SelectedParentGenre = genre.ParentId
            };

            // Act
            var result = await sut.Update(editGenreViewModel.Id, editGenreViewModel);

            // Assert
            result.Should().BeOfType<RedirectToActionResult>()
                .Subject.ActionName.Should().BeEquivalentTo(nameof(sut.GetGenres));

            mockGenreService.Verify(x => x.EditGenreAsync(It.IsAny<GenreModel>()), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public async Task Update_Post_ReturnsNotFoundResult_WhenGenreIsNotFound(
            Guid id,
            EditGenreViewModel editGenreViewModel,
            GenreController sut)
        {
            // Act
            var result = await sut.Update(id, editGenreViewModel);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }
        
        [Theory]
        [AutoMoqData]
        public async Task Update_Post_ReturnsViewResult_WhenGenreIsInvalid(
            EditGenreViewModel editGenreViewModel,
            GenreController sut)
        {
            // Arrange
            sut.ModelState.AddModelError("Name", "Required");

            // Act
            var result = await sut.Update(editGenreViewModel.Id, editGenreViewModel);

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<EditGenreViewModel>()
                .Which.Id.Should().Be(editGenreViewModel.Id);
        }
    }
}