using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineGameStore.BLL.Services.Contracts;
using OnlineGameStore.MVC.Controllers;
using OnlineGameStore.Tests.Helpers;
using Xunit;

namespace OnlineGameStore.Tests.Controllers
{
    public partial class GenreControllerTests
    {
        [Theory]
        [AutoMoqData]
        public void Remove_ReturnsRedirectToActionResult_WhenIdHasValue(
            int id,
            [Frozen] Mock<IGenreService> mockGenreService,
            GenreController sut)
        {
            // Arrange
            mockGenreService.Setup(x => x.DeleteGenre(It.IsAny<int>()));

            // Act
            var result = sut.Remove(id);

            // Assert
            result.Should().BeOfType<RedirectToActionResult>()
                .Subject.ActionName.Should().BeEquivalentTo(nameof(sut.GetGenres));

            mockGenreService.Verify(x => x.DeleteGenre(It.IsAny<int>()), Times.Once);
        }

        [Theory]
        [InlineAutoMoqData(null)]
        public void Remove_ReturnsBadRequestResult_WhenIdHasNotValue(
            int? id,
            GenreController sut)
        {
            // Act
            var result = sut.Remove(id);

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }
    }
}