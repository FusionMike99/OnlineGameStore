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
    public partial class PlatformTypeControllerTests
    {
        [Theory]
        [AutoMoqData]
        public void GetPlatformTypeById_ReturnsViewResult_WhenPlatformTypeIdHasValue(
            PlatformType platformType,
            [Frozen] Mock<IPlatformTypeService> mockGenreService,
            PlatformTypeController sut)
        {
            // Arrange
            mockGenreService.Setup(x => x.GetPlatformTypeById(It.IsAny<int>()))
                .Returns(platformType);

            // Act
            var result = sut.GetPlatformTypeById(platformType.Id);

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<PlatformTypeViewModel>()
                .Which.Id.Should().Be(platformType.Id);

            mockGenreService.Verify(x => x.GetPlatformTypeById(It.IsAny<int>()), Times.Once);
        }

        [Theory]
        [InlineAutoMoqData(null)]
        public void GetPlatformTypeById_ReturnsBadRequestObjectResult_WhenPlatformTypeIdHasNotValue(
            int? platformTypeId,
            PlatformTypeController sut)
        {
            // Act
            var result = sut.GetPlatformTypeById(platformTypeId);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>()
                .Which.Value.Should().BeOfType<string>();
        }

        [Theory]
        [InlineAutoMoqData(null)]
        public void GetPlatformTypeById_ReturnsNotFoundObjectResult_WhenPlatformTypeIsNotFound(
            PlatformType platformType,
            int platformTypeId,
            [Frozen] Mock<IPlatformTypeService> mockPlatformTypeService,
            PlatformTypeController sut)
        {
            // Arrange
            mockPlatformTypeService.Setup(x => x.GetPlatformTypeById(It.IsAny<int>()))
                .Returns(platformType);

            // Act
            var result = sut.GetPlatformTypeById(platformTypeId);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>()
                .Which.Value.Should().BeOfType<string>();

            mockPlatformTypeService.Verify(x => x.GetPlatformTypeById(It.IsAny<int>()), Times.Once);
        }
    }
}