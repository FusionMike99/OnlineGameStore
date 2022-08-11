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
        public async Task GetGenreById_ReturnsViewResult_WhenGenreIdHasValue(
            GenreModel genre,
            [Frozen] Mock<IGenreService> mockGenreService,
            GenreController sut)
        {
            // Arrange
            mockGenreService.Setup(x => x.GetGenreById(It.IsAny<Guid>()))
                .ReturnsAsync(genre);

            // Act
            var result = await sut.GetGenreById(genre.Id);

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<GenreViewModel>()
                .Which.Id.Should().Be(genre.Id);

            mockGenreService.Verify(x => x.GetGenreById(It.IsAny<Guid>()), Times.Once);
        }

        [Theory]
        [InlineAutoMoqData(null)]
        public async Task GetGenreById_ReturnsNotFoundResult_WhenGenreIsNotFound(
            GenreModel genre,
            Guid genreId,
            [Frozen] Mock<IGenreService> mockGenreService,
            GenreController sut)
        {
            // Arrange
            mockGenreService.Setup(x => x.GetGenreById(It.IsAny<Guid>()))
                .ReturnsAsync(genre);

            // Act
            var result = await sut.GetGenreById(genreId);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            mockGenreService.Verify(x => x.GetGenreById(It.IsAny<Guid>()), Times.Once);
        }
    }
}