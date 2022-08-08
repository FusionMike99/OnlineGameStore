using System.Collections.Generic;
using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineGameStore.BLL.Entities.Northwind;
using OnlineGameStore.BLL.Services.Contracts;
using OnlineGameStore.MVC.Controllers;
using OnlineGameStore.MVC.Models;
using OnlineGameStore.Tests.Helpers;
using Xunit;

namespace OnlineGameStore.Tests.Controllers.ShipperControllerTests
{
    public class ShipperControllerTests_GetShippers
    {
        [Theory]
        [AutoMoqData]
        public void GetShippers_ReturnsViewResult(
            List<NorthwindShipper> shippers,
            [Frozen] Mock<IShipperService> mockShipperService,
            ShipperController sut)
        {
            // Arrange
            mockShipperService.Setup(x => x.GetAllShippersAsync())
                .Returns(shippers);

            // Act
            var result = sut.GetShippers();

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<IEnumerable<ShipperViewModel>>()
                .Which.Should().HaveSameCount(shippers);

            mockShipperService.Verify(x => x.GetAllShippersAsync(), Times.Once);
        }
    }
}