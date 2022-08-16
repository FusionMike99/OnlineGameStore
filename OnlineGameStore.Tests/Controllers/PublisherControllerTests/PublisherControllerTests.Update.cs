using System.Collections.Generic;
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
    public partial class PublisherControllerTests
    {
        [Theory]
        [AutoMoqData]
        public async Task Update_Get_ReturnsViewResult(
            PublisherModel publisher,
            [Frozen] Mock<IPublisherService> mockPublisherService,
            PublisherController sut)
        {
            // Arrange
            mockPublisherService.Setup(x => x.GetPublisherByCompanyNameAsync(It.IsAny<string>()))
                .ReturnsAsync(publisher);

            // Act
            var result = await sut.Update(publisher.CompanyName);

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<EditPublisherViewModel>()
                .Which.Id.Should().Be(publisher.Id);
            
            mockPublisherService.Verify(x => x.GetPublisherByCompanyNameAsync(It.IsAny<string>()), Times.Once);
        }

        [Theory]
        [InlineAutoMoqData("")]
        [InlineAutoMoqData(" ")]
        [InlineAutoMoqData(null)]
        public async Task Update_Get_ReturnsBadRequestResult_WhenPublisherCompanyNameHasNotValue(
            string companyName,
            PublisherController sut)
        {
            // Act
            var result = await sut.Update(companyName);

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Theory]
        [InlineAutoMoqData(null)]
        public async Task Update_Get_ReturnsNotFoundResult_WhenPublisherIsNotFound(
            PublisherModel publisher,
            string companyName,
            [Frozen] Mock<IPublisherService> mockPublisherService,
            PublisherController sut)
        {
            // Arrange
            mockPublisherService.Setup(x => x.GetPublisherByCompanyNameAsync(It.IsAny<string>()))
                .ReturnsAsync(publisher);

            // Act
            var result = await sut.Update(companyName);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            mockPublisherService.Verify(x => x.GetPublisherByCompanyNameAsync(It.IsAny<string>()), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public async Task Update_Post_ReturnsRedirectToActionResult_WhenPublisherIsValid(
            PublisherModel publisher,
            [Frozen] Mock<IPublisherService> mockPublisherService,
            PublisherController sut)
        {
            // Arrange
            mockPublisherService.Setup(x => x.EditPublisherAsync(It.IsAny<PublisherModel>()))
                .ReturnsAsync(publisher);

            var editPublisherViewModel = new EditPublisherViewModel
            {
                Id = publisher.Id,
                CompanyName = publisher.CompanyName,
                Description = publisher.Description,
                HomePage = publisher.HomePage
            };

            // Act
            var result = await sut.Update(editPublisherViewModel.CompanyName, editPublisherViewModel);

            // Assert
            var redirectToActionResult = result.Should().BeOfType<RedirectToActionResult>().Subject;

            redirectToActionResult.ActionName.Should().BeEquivalentTo(nameof(sut.GetPublisherByCompanyName));

            redirectToActionResult.RouteValues.Should()
                .Contain(new KeyValuePair<string, object>("companyName", editPublisherViewModel.CompanyName));

            mockPublisherService.Verify(x => x.EditPublisherAsync(It.IsAny<PublisherModel>()), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public async Task Update_Post_ReturnsViewResult_WhenGenreIsInvalid(
            EditPublisherViewModel editPublisherViewModel,
            PublisherController sut)
        {
            // Arrange
            sut.ModelState.AddModelError("Name", "Required");

            // Act
            var result = await sut.Update(editPublisherViewModel.CompanyName, editPublisherViewModel);

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<EditPublisherViewModel>()
                .Which.Id.Should().Be(editPublisherViewModel.Id);
        }
    }
}