using System.Collections.Generic;
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
    public partial class PlatformTypeControllerTests
    {
        [Theory]
        [AutoMoqData]
        public async Task GetPlatformTypes_ReturnsViewResult(
            List<PlatformTypeModel> platformTypes,
            [Frozen] Mock<IPlatformTypeService> mockPlatformTypeService,
            PlatformTypeController sut)
        {
            // Arrange
            mockPlatformTypeService.Setup(x => x.GetAllPlatformTypesAsync())
                .ReturnsAsync(platformTypes);

            // Act
            var result = await sut.GetPlatformTypes();

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<IEnumerable<PlatformTypeViewModel>>()
                .Which.Should().HaveSameCount(platformTypes);

            mockPlatformTypeService.Verify(x => x.GetAllPlatformTypesAsync(), Times.Once);
        }
    }
}