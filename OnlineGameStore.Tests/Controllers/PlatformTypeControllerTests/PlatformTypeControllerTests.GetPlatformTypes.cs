using System.Collections.Generic;
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
        public void GetPlatformTypes_ReturnsViewResult(
            IEnumerable<PlatformType> platformTypes,
            [Frozen] Mock<IPlatformTypeService> mockPlatformTypeService,
            PlatformTypeController sut)
        {
            // Arrange
            mockPlatformTypeService.Setup(x => x.GetAllPlatformTypes())
                .Returns(platformTypes);

            // Act
            var result = sut.GetPlatformTypes();

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<IEnumerable<PlatformTypeViewModel>>()
                .Which.Should().HaveSameCount(platformTypes);

            mockPlatformTypeService.Verify(x => x.GetAllPlatformTypes(), Times.Once);
        }
    }
}