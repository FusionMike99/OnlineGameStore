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
    public partial class PublisherControllerTests
    {
        [Theory]
        [AutoMoqData]
        public void Create_Get_ReturnsViewResult(
            PublisherController sut)
        {
            // Act
            var result = sut.Create();

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<EditPublisherViewModel>();
        }

        [Theory]
        [AutoMoqData]
        public async Task Create_Post_ReturnsRedirectToActionResult_WhenPublisherIsValid(
            PublisherModel publisher,
            [Frozen] Mock<IPublisherService> mockPublisherService,
            PublisherController sut)
        {
            // Arrange
            mockPublisherService.Setup(x => x.CreatePublisherAsync(It.IsAny<PublisherModel>()))
                .ReturnsAsync(publisher);

            var editPublisherViewModel = new EditPublisherViewModel
            {
                Id = publisher.Id,
                CompanyName = publisher.CompanyName,
                Description = publisher.Description,
                HomePage = publisher.HomePage
            };

            // Act
            var result = await sut.Create(editPublisherViewModel);

            // Assert
            var redirectToActionResult = result.Should().BeOfType<RedirectToActionResult>().Subject;

            redirectToActionResult.ActionName.Should().BeEquivalentTo(nameof(sut.GetPublisherByCompanyName));

            redirectToActionResult.RouteValues.Should()
                .Contain(new KeyValuePair<string, object>("companyName", editPublisherViewModel.CompanyName));

            mockPublisherService.Verify(x => x.CreatePublisherAsync(It.IsAny<PublisherModel>()), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public async Task Create_Post_ReturnsViewResult_WhenGenreIsInvalid(
            EditPublisherViewModel editPublisherViewModel,
            PublisherController sut)
        {
            // Arrange
            sut.ModelState.AddModelError("CompanyName", "Required");

            // Act
            var result = await sut.Create(editPublisherViewModel);

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<EditPublisherViewModel>()
                .Which.Id.Should().Be(editPublisherViewModel.Id);
        }
    }
}