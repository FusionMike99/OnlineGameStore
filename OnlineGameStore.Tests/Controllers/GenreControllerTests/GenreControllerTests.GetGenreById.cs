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
        public void GetGenreById_ReturnsViewResult_WhenGenreIdHasValue(
            Genre genre,
            [Frozen] Mock<IGenreService> mockGenreService,
            GenreController sut)
        {
            // Arrange
            mockGenreService.Setup(x => x.GetGenreById(It.IsAny<int>()))
                .Returns(genre);

            // Act
            var result = sut.GetGenreById(genre.Id);

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<GenreViewModel>()
                .Which.Id.Should().Be(genre.Id);

            mockGenreService.Verify(x => x.GetGenreById(It.IsAny<int>()), Times.Once);
        }

        [Theory]
        [InlineAutoMoqData(null)]
        public void GetGenreById_ReturnsBadRequestObjectResult_WhenGenreIdHasNotValue(
            int? genreId,
            GenreController sut)
        {
            // Act
            var result = sut.GetGenreById(genreId);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>()
                .Which.Value.Should().BeOfType<string>();
        }

        [Theory]
        [InlineAutoMoqData(null)]
        public void GetGenreById_ReturnsNotFoundObjectResult_WhenGenreIsNotFound(
            Genre genre,
            int genreId,
            [Frozen] Mock<IGenreService> mockGenreService,
            GenreController sut)
        {
            // Arrange
            mockGenreService.Setup(x => x.GetGenreById(It.IsAny<int>()))
                .Returns(genre);

            // Act
            var result = sut.GetGenreById(genreId);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>()
                .Which.Value.Should().BeOfType<string>();

            mockGenreService.Verify(x => x.GetGenreById(It.IsAny<int>()), Times.Once);
        }
    }
}