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
        public async Task Update_Get_ReturnsViewResult(
            PlatformTypeModel platformType,
            [Frozen] Mock<IPlatformTypeService> mockPlatformTypeService,
            PlatformTypeController sut)
        {
            // Arrange
            mockPlatformTypeService.Setup(x => x.GetPlatformTypeByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(platformType);

            // Act
            var result = await sut.Update(platformType.Id);

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<EditPlatformTypeViewModel>()
                .Which.Id.Should().Be(platformType.Id);
            
            mockPlatformTypeService.Verify(x => x.GetPlatformTypeByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Theory]
        [InlineAutoMoqData(null)]
        public async Task Update_Get_ReturnsNotFoundResult_WhenPlatformTypeIsNotFound(
            PlatformTypeModel platformType,
            Guid platformTypeId,
            [Frozen] Mock<IPlatformTypeService> mockPlatformTypeService,
            PlatformTypeController sut)
        {
            // Arrange
            mockPlatformTypeService.Setup(x => x.GetPlatformTypeByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(platformType);

            // Act
            var result = await sut.Update(platformTypeId);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            mockPlatformTypeService.Verify(x => x.GetPlatformTypeByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public async Task Update_Post_ReturnsRedirectToActionResult_WhenPlatformTypeIsValid(
            PlatformTypeModel platformType,
            [Frozen] Mock<IPlatformTypeService> mockPlatformTypeService,
            PlatformTypeController sut)
        {
            // Arrange
            mockPlatformTypeService.Setup(x => x.EditPlatformTypeAsync(It.IsAny<PlatformTypeModel>()))
                .ReturnsAsync(platformType);

            var editPlatformTypeViewModel = new EditPlatformTypeViewModel
            {
                Id = platformType.Id,
                Type = platformType.Type
            };

            // Act
            var result = await sut.Update(editPlatformTypeViewModel.Id,editPlatformTypeViewModel);

            // Assert
            result.Should().BeOfType<RedirectToActionResult>()
                .Subject.ActionName.Should().BeEquivalentTo(nameof(sut.GetPlatformTypes));

            mockPlatformTypeService.Verify(x => x.EditPlatformTypeAsync(It.IsAny<PlatformTypeModel>()), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public async Task Update_Post_ReturnsNotFoundResult_WhenPlatformTypeIsNotFound(
            Guid id,
            EditPlatformTypeViewModel editPlatformTypeViewModel,
            PlatformTypeController sut)
        {
            // Act
            var result = await sut.Update(id, editPlatformTypeViewModel);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }
        
        [Theory]
        [AutoMoqData]
        public async Task Update_Post_ReturnsViewResult_WhenPlatformTypeIsInvalid(
            EditPlatformTypeViewModel editPlatformTypeViewModel,
            PlatformTypeController sut)
        {
            // Arrange
            sut.ModelState.AddModelError("Type", "Required");

            // Act
            var result = await sut.Update(editPlatformTypeViewModel.Id, editPlatformTypeViewModel);

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<EditPlatformTypeViewModel>()
                .Which.Id.Should().Be(editPlatformTypeViewModel.Id);
        }
    }
}