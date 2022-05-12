using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineGameStore.BLL.Services.Contracts;
using OnlineGameStore.MVC.Controllers;
using OnlineGameStore.Tests.Helpers;
using Xunit;

namespace OnlineGameStore.Tests.Controllers
{
    public partial class PublisherControllerTests
    {
        [Theory]
        [AutoMoqData]
        public void Remove_ReturnsRedirectToActionResult_WhenIdHasValue(
            int id,
            [Frozen] Mock<IPublisherService> mockPublisherService,
            PublisherController sut)
        {
            // Arrange
            mockPublisherService.Setup(x => x.DeletePublisher(It.IsAny<int>()));

            // Act
            var result = sut.Remove(id);

            // Assert
            result.Should().BeOfType<RedirectToActionResult>()
                .Subject.ActionName.Should().BeEquivalentTo(nameof(sut.GetPublishers));

            mockPublisherService.Verify(x => x.DeletePublisher(It.IsAny<int>()), Times.Once);
        }

        [Theory]
        [InlineAutoMoqData(null)]
        public void Remove_ReturnsBadRequestObjectResult_WhenIdHasNotValue(
            int? id,
            PublisherController sut)
        {
            // Act
            var result = sut.Remove(id);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>()
                .Which.Value.Should().BeOfType<string>();
        }
    }
}