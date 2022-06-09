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
    public partial class GenreControllerTests
    {
        [Theory]
        [AutoMoqData]
        public void Update_Get_ReturnsViewResult(
            Genre genre,
            [Frozen] Mock<IGenreService> mockGenreService,
            GenreController sut)
        {
            // Arrange
            mockGenreService.Setup(x => x.GetGenreById(It.IsAny<int>()))
                .Returns(genre);

            // Act
            var result = sut.Update(genre.Id);

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<EditGenreViewModel>()
                .Which.Id.Should().Be(genre.Id);
            
            mockGenreService.Verify(x => x.GetGenreById(It.IsAny<int>()), Times.Once);
        }

        [Theory]
        [InlineAutoMoqData(null)]
        public void Update_Get_ReturnsBadRequestResult_WhenGenreIdHasNotValue(
            int? genreId,
            GenreController sut)
        {
            // Act
            var result = sut.Update(genreId);

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Theory]
        [InlineAutoMoqData(null)]
        public void Update_Get_ReturnsNotFoundResult_WhenGenreIsNotFound(
            Genre genre,
            int? genreId,
            [Frozen] Mock<IGenreService> mockGenreService,
            GenreController sut)
        {
            // Arrange
            mockGenreService.Setup(x => x.GetGenreById(It.IsAny<int>()))
                .Returns(genre);

            // Act
            var result = sut.Update(genreId);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            mockGenreService.Verify(x => x.GetGenreById(It.IsAny<int>()), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public void Update_Post_ReturnsRedirectToActionResult_WhenGenreIsValid(
            Genre genre,
            [Frozen] Mock<IGenreService> mockGenreService,
            GenreController sut)
        {
            // Arrange
            mockGenreService.Setup(x => x.EditGenre(It.IsAny<Genre>()))
                .Returns(genre);

            var editGenreViewModel = new EditGenreViewModel
            {
                Id = genre.Id,
                Name = genre.Name,
                SelectedParentGenre = genre.ParentId
            };

            // Act
            var result = sut.Update(editGenreViewModel.Id, editGenreViewModel);

            // Assert
            result.Should().BeOfType<RedirectToActionResult>()
                .Subject.ActionName.Should().BeEquivalentTo(nameof(sut.GetGenres));

            mockGenreService.Verify(x => x.EditGenre(It.IsAny<Genre>()), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public void Update_Post_ReturnsNotFoundResult_WhenGenreIsNotFound(
            EditGenreViewModel editGenreViewModel,
            GenreController sut)
        {
            // Arrange
            var id = editGenreViewModel.Id - 1;

            // Act
            var result = sut.Update(id, editGenreViewModel);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }
        
        [Theory]
        [AutoMoqData]
        public void Update_Post_ReturnsViewResult_WhenGenreIsInvalid(
            EditGenreViewModel editGenreViewModel,
            GenreController sut)
        {
            // Arrange
            sut.ModelState.AddModelError("Name", "Required");

            // Act
            var result = sut.Update(editGenreViewModel.Id, editGenreViewModel);

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<EditGenreViewModel>()
                .Which.Id.Should().Be(editGenreViewModel.Id);
        }
    }
}