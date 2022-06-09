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
    public partial class PublisherControllerTests
    {
        [Theory]
        [AutoMoqData]
        public void GetPublisherByCompanyName_ReturnsViewResult_WhenPublisherCompanyNameHasValue(
            Publisher publisher,
            [Frozen] Mock<IPublisherService> mockPublisherService,
            PublisherController sut)
        {
            // Arrange
            mockPublisherService.Setup(x => x.GetPublisherByCompanyName(It.IsAny<string>()))
                .Returns(publisher);

            // Act
            var result = sut.GetPublisherByCompanyName(publisher.CompanyName);

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<PublisherViewModel>()
                .Which.CompanyName.Should().Be(publisher.CompanyName);

            mockPublisherService.Verify(x => x.GetPublisherByCompanyName(It.IsAny<string>()), Times.Once);
        }

        [Theory]
        [InlineAutoMoqData("")]
        [InlineAutoMoqData(" ")]
        [InlineAutoMoqData(null)]
        public void GetPublisherByCompanyName_ReturnsBadRequestResult_WhenPublisherCompanyNameHasNotValue(
            string companyName,
            PublisherController sut)
        {
            // Act
            var result = sut.GetPublisherByCompanyName(companyName);

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Theory]
        [InlineAutoMoqData(null)]
        public void GetPublisherByCompanyName_ReturnsNotFoundResult_WhenPublisherIsNotFound(
            Publisher publisher,
            string companyName,
            [Frozen] Mock<IPublisherService> mockPublisherService,
            PublisherController sut)
        {
            // Arrange
            mockPublisherService.Setup(x => x.GetPublisherByCompanyName(It.IsAny<string>()))
                .Returns(publisher);

            // Act
            var result = sut.GetPublisherByCompanyName(companyName);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            mockPublisherService.Verify(x => x.GetPublisherByCompanyName(It.IsAny<string>()), Times.Once);
        }
    }
}