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
        public void Update_Get_ReturnsViewResult(
            PlatformType platformType,
            [Frozen] Mock<IPlatformTypeService> mockPlatformTypeService,
            PlatformTypeController sut)
        {
            // Arrange
            mockPlatformTypeService.Setup(x => x.GetPlatformTypeById(It.IsAny<int>()))
                .Returns(platformType);

            // Act
            var result = sut.Update(platformType.Id);

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<EditPlatformTypeViewModel>()
                .Which.Id.Should().Be(platformType.Id);
            
            mockPlatformTypeService.Verify(x => x.GetPlatformTypeById(It.IsAny<int>()), Times.Once);
        }

        [Theory]
        [InlineAutoMoqData(null)]
        public void Update_Get_ReturnsBadRequestResult_WhenPlatformTypeIdHasNotValue(
            int? platformTypeId,
            PlatformTypeController sut)
        {
            // Act
            var result = sut.Update(platformTypeId);

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Theory]
        [InlineAutoMoqData(null)]
        public void Update_Get_ReturnsNotFoundResult_WhenPlatformTypeIsNotFound(
            PlatformType platformType,
            int? platformTypeId,
            [Frozen] Mock<IPlatformTypeService> mockPlatformTypeService,
            PlatformTypeController sut)
        {
            // Arrange
            mockPlatformTypeService.Setup(x => x.GetPlatformTypeById(It.IsAny<int>()))
                .Returns(platformType);

            // Act
            var result = sut.Update(platformTypeId);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            mockPlatformTypeService.Verify(x => x.GetPlatformTypeById(It.IsAny<int>()), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public void Update_Post_ReturnsRedirectToActionResult_WhenPlatformTypeIsValid(
            PlatformType platformType,
            [Frozen] Mock<IPlatformTypeService> mockPlatformTypeService,
            PlatformTypeController sut)
        {
            // Arrange
            mockPlatformTypeService.Setup(x => x.EditPlatformType(It.IsAny<PlatformType>()))
                .Returns(platformType);

            var editPlatformTypeViewModel = new EditPlatformTypeViewModel
            {
                Id = platformType.Id,
                Type = platformType.Type
            };

            // Act
            var result = sut.Update(editPlatformTypeViewModel.Id,editPlatformTypeViewModel);

            // Assert
            result.Should().BeOfType<RedirectToActionResult>()
                .Subject.ActionName.Should().BeEquivalentTo(nameof(sut.GetPlatformTypes));

            mockPlatformTypeService.Verify(x => x.EditPlatformType(It.IsAny<PlatformType>()), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public void Update_Post_ReturnsNotFoundResult_WhenPlatformTypeIsNotFound(
            EditPlatformTypeViewModel editPlatformTypeViewModel,
            PlatformTypeController sut)
        {
            // Arrange
            var id = editPlatformTypeViewModel.Id - 1;

            // Act
            var result = sut.Update(id, editPlatformTypeViewModel);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }
        
        [Theory]
        [AutoMoqData]
        public void Update_Post_ReturnsViewResult_WhenPlatformTypeIsInvalid(
            EditPlatformTypeViewModel editPlatformTypeViewModel,
            PlatformTypeController sut)
        {
            // Arrange
            sut.ModelState.AddModelError("Type", "Required");

            // Act
            var result = sut.Update(editPlatformTypeViewModel.Id, editPlatformTypeViewModel);

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<EditPlatformTypeViewModel>()
                .Which.Id.Should().Be(editPlatformTypeViewModel.Id);
        }
    }
}