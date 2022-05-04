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
        public void Create_Get_ReturnsViewResult(
            PlatformTypeController sut)
        {
            // Act
            var result = sut.Create();

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<EditPlatformTypeViewModel>();
        }

        [Theory]
        [AutoMoqData]
        public void Create_Post_ReturnsRedirectToActionResult_WhenPlatformTypeIsValid(
            PlatformType platformType,
            [Frozen] Mock<IPlatformTypeService> mockPlatformTypeService,
            PlatformTypeController sut)
        {
            // Arrange
            mockPlatformTypeService.Setup(x => x.CreatePlatformType(It.IsAny<PlatformType>()))
                .Returns(platformType);

            var editPlatformTypeViewModel = new EditPlatformTypeViewModel
            {
                Id = platformType.Id,
                Type = platformType.Type
            };

            // Act
            var result = sut.Create(editPlatformTypeViewModel);

            // Assert
            result.Should().BeOfType<RedirectToActionResult>()
                .Subject.ActionName.Should().BeEquivalentTo(nameof(sut.GetPlatformTypes));

            mockPlatformTypeService.Verify(x => x.CreatePlatformType(It.IsAny<PlatformType>()), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public void Create_Post_ReturnsViewResult_WhenPlatformTypeIsInvalid(
            EditPlatformTypeViewModel editPlatformTypeViewModel,
            PlatformTypeController sut)
        {
            // Arrange
            sut.ModelState.AddModelError("Type", "Required");

            // Act
            var result = sut.Create(editPlatformTypeViewModel);

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<EditPlatformTypeViewModel>()
                    .Which.Id.Should().Be(editPlatformTypeViewModel.Id);
        }
    }
}
