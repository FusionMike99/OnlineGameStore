﻿using AutoFixture.Xunit2;
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
        }

        [Theory]
        [InlineAutoMoqData(null)]
        public void Update_Get_ReturnsBadRequestObjectResult_WhenPlatformTypeIdHasNotValue(
            int? platformTypeId,
            PlatformTypeController sut)
        {
            // Act
            var result = sut.Update(platformTypeId);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>()
                .Which.Value.Should().BeOfType<string>();
        }

        [Theory]
        [InlineAutoMoqData(null)]
        public void Update_Get_ReturnsNotFoundObjectResult_WhenPlatformTypeIsNotFound(
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
            result.Should().BeOfType<NotFoundObjectResult>()
                .Which.Value.Should().BeOfType<string>();

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
            var result = sut.Update(editPlatformTypeViewModel);

            // Assert
            result.Should().BeOfType<RedirectToActionResult>()
                .Subject.ActionName.Should().BeEquivalentTo(nameof(sut.GetPlatformTypes));

            mockPlatformTypeService.Verify(x => x.EditPlatformType(It.IsAny<PlatformType>()), Times.Once);
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
            var result = sut.Update(editPlatformTypeViewModel);

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<EditPlatformTypeViewModel>()
                    .Which.Id.Should().Be(editPlatformTypeViewModel.Id);
        }
    }
}
