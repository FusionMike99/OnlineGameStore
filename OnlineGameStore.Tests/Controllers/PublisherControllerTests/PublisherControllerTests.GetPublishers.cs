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
        public async Task GetPublishers_ReturnsViewResult(
            List<PublisherModel> publishers,
            [Frozen] Mock<IPublisherService> mockPublisherService,
            PublisherController sut)
        {
            // Arrange
            mockPublisherService.Setup(x => x.GetAllPublishersAsync())
                .ReturnsAsync(publishers);

            // Act
            var result = await sut.GetPublishers();

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<IEnumerable<PublisherViewModel>>()
                .Which.Should().HaveSameCount(publishers);

            mockPublisherService.Verify(x => x.GetAllPublishersAsync(), Times.Once);
        }
    }
}