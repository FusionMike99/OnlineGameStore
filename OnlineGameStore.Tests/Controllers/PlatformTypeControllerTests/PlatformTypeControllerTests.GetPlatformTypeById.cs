using System;
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
    public partial class PlatformTypeControllerTests
    {
        [Theory]
        [AutoMoqData]
        public async Task GetPlatformTypeById_ReturnsViewResult_WhenPlatformTypeIdHasValue(
            PlatformTypeModel platformType,
            [Frozen] Mock<IPlatformTypeService> mockGenreService,
            PlatformTypeController sut)
        {
            // Arrange
            mockGenreService.Setup(x => x.GetPlatformTypeByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(platformType);

            // Act
            var result = await sut.GetPlatformTypeById(platformType.Id);

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<PlatformTypeViewModel>()
                .Which.Id.Should().Be(platformType.Id);

            mockGenreService.Verify(x => x.GetPlatformTypeByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Theory]
        [InlineAutoMoqData(null)]
        public async Task GetPlatformTypeById_ReturnsNotFoundResult_WhenPlatformTypeIsNotFound(
            PlatformTypeModel platformType,
            Guid platformTypeId,
            [Frozen] Mock<IPlatformTypeService> mockPlatformTypeService,
            PlatformTypeController sut)
        {
            // Arrange
            mockPlatformTypeService.Setup(x => x.GetPlatformTypeByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(platformType);

            // Act
            var result = await sut.GetPlatformTypeById(platformTypeId);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            mockPlatformTypeService.Verify(x => x.GetPlatformTypeByIdAsync(It.IsAny<Guid>()), Times.Once);
        }
    }
}