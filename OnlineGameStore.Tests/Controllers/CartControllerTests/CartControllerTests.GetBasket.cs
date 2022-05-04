using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.MVC.Services.Contracts;
using OnlineGameStore.MVC.Controllers;
using OnlineGameStore.MVC.Models;
using OnlineGameStore.Tests.Helpers;
using System.Collections.Generic;
using Xunit;

namespace OnlineGameStore.Tests.Controllers
{
    public partial class CartControllerTests
    {
        [Theory]
        [AutoMoqData]
        public void GetGames_ReturnsViewResult(
            List<OrderDetail> orderDetails,
            [Frozen] Mock<ICartService> mockCartService,
            CartController sut)
        {
            // Arrange
            mockCartService.SetupGet(x => x.OrderDetails)
                .Returns(orderDetails);

            // Act
            var result = sut.GetBasket();

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<IEnumerable<OrderDetailViewModel>>()
                .Which.Should().HaveSameCount(orderDetails);

            mockCartService.Verify(x => x.OrderDetails, Times.Once);
        }
    }
}
