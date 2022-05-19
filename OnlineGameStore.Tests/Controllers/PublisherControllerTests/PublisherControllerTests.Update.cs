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
    public partial class PublisherControllerTests
    {
        [Theory]
        [AutoMoqData]
        public void Update_Get_ReturnsViewResult(
            Publisher publisher,
            [Frozen] Mock<IPublisherService> mockPublisherService,
            PublisherController sut)
        {
            // Arrange
            mockPublisherService.Setup(x => x.GetPublisherByCompanyName(It.IsAny<string>()))
                .Returns(publisher);

            // Act
            var result = sut.Update(publisher.CompanyName);

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<EditPublisherViewModel>()
                .Which.Id.Should().Be(publisher.Id);
            
            mockPublisherService.Verify(x => x.GetPublisherByCompanyName(It.IsAny<string>()), Times.Once);
        }

        [Theory]
        [InlineAutoMoqData("")]
        [InlineAutoMoqData(" ")]
        [InlineAutoMoqData(null)]
        public void Update_Get_ReturnsBadRequestObjectResult_WhenPublisherCompanyNameHasNotValue(
            string companyName,
            PublisherController sut)
        {
            // Act
            var result = sut.Update(companyName);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>()
                .Which.Value.Should().BeOfType<string>();
        }

        [Theory]
        [InlineAutoMoqData(null)]
        public void Update_Get_ReturnsNotFoundObjectResult_WhenPublisherIsNotFound(
            Publisher publisher,
            string companyName,
            [Frozen] Mock<IPublisherService> mockPublisherService,
            PublisherController sut)
        {
            // Arrange
            mockPublisherService.Setup(x => x.GetPublisherByCompanyName(It.IsAny<string>()))
                .Returns(publisher);

            // Act
            var result = sut.Update(companyName);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>()
                .Which.Value.Should().BeOfType<string>();

            mockPublisherService.Verify(x => x.GetPublisherByCompanyName(It.IsAny<string>()), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public void Update_Post_ReturnsRedirectToActionResult_WhenPublisherIsValid(
            Publisher publisher,
            [Frozen] Mock<IPublisherService> mockPublisherService,
            PublisherController sut)
        {
            // Arrange
            mockPublisherService.Setup(x => x.EditPublisher(It.IsAny<Publisher>()))
                .Returns(publisher);

            var editPublisherViewModel = new EditPublisherViewModel
            {
                Id = publisher.Id,
                CompanyName = publisher.CompanyName,
                Description = publisher.Description,
                HomePage = publisher.HomePage
            };

            // Act
            var result = sut.Update(editPublisherViewModel.CompanyName, editPublisherViewModel);

            // Assert
            var redirectToActionResult = result.Should().BeOfType<RedirectToActionResult>().Subject;

            redirectToActionResult.ActionName.Should().BeEquivalentTo(nameof(sut.GetPublisherByCompanyName));

            redirectToActionResult.RouteValues.Should()
                .Contain(new KeyValuePair<string, object>("companyName", editPublisherViewModel.CompanyName));

            mockPublisherService.Verify(x => x.EditPublisher(It.IsAny<Publisher>()), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public void Update_Post_ReturnsViewResult_WhenGenreIsInvalid(
            EditPublisherViewModel editPublisherViewModel,
            PublisherController sut)
        {
            // Arrange
            sut.ModelState.AddModelError("Name", "Required");

            // Act
            var result = sut.Update(editPublisherViewModel.CompanyName, editPublisherViewModel);

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<EditPublisherViewModel>()
                .Which.Id.Should().Be(editPublisherViewModel.Id);
        }
    }
}