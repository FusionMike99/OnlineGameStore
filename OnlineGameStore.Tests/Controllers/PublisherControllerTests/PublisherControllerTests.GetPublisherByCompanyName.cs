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
    public partial class PublisherControllerTests
    {
        [Theory]
        [AutoMoqData]
        public async Task GetPublisherByCompanyName_ReturnsViewResult_WhenPublisherCompanyNameHasValue(
            PublisherModel publisher,
            [Frozen] Mock<IPublisherService> mockPublisherService,
            PublisherController sut)
        {
            // Arrange
            mockPublisherService.Setup(x => x.GetPublisherByCompanyNameAsync(It.IsAny<string>()))
                .ReturnsAsync(publisher);

            // Act
            var result = await sut.GetPublisherByCompanyName(publisher.CompanyName);

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<PublisherViewModel>()
                .Which.CompanyName.Should().Be(publisher.CompanyName);

            mockPublisherService.Verify(x => x.GetPublisherByCompanyNameAsync(It.IsAny<string>()), Times.Once);
        }

        [Theory]
        [InlineAutoMoqData("")]
        [InlineAutoMoqData(" ")]
        [InlineAutoMoqData(null)]
        public async Task GetPublisherByCompanyName_ReturnsBadRequestResult_WhenPublisherCompanyNameHasNotValue(
            string companyName,
            PublisherController sut)
        {
            // Act
            var result = await sut.GetPublisherByCompanyName(companyName);

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Theory]
        [InlineAutoMoqData(null)]
        public async Task GetPublisherByCompanyName_ReturnsNotFoundResult_WhenPublisherIsNotFound(
            PublisherModel publisher,
            string companyName,
            [Frozen] Mock<IPublisherService> mockPublisherService,
            PublisherController sut)
        {
            // Arrange
            mockPublisherService.Setup(x => x.GetPublisherByCompanyNameAsync(It.IsAny<string>()))
                .ReturnsAsync(publisher);

            // Act
            var result = await sut.GetPublisherByCompanyName(companyName);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            mockPublisherService.Verify(x => x.GetPublisherByCompanyNameAsync(It.IsAny<string>()), Times.Once);
        }
    }
}