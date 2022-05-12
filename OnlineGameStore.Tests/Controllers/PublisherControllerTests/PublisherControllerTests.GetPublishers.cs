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
        public void GetPublishers_ReturnsViewResult(
            IEnumerable<Publisher> publishers,
            [Frozen] Mock<IPublisherService> mockPublisherService,
            PublisherController sut)
        {
            // Arrange
            mockPublisherService.Setup(x => x.GetAllPublishers())
                .Returns(publishers);

            // Act
            var result = sut.GetPublishers();

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<IEnumerable<PublisherViewModel>>()
                .Which.Should().HaveSameCount(publishers);

            mockPublisherService.Verify(x => x.GetAllPublishers(), Times.Once);
        }
    }
}