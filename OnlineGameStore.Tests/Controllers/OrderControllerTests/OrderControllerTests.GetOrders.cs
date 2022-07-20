using System.Collections.Generic;
using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Models;
using OnlineGameStore.BLL.Services.Contracts;
using OnlineGameStore.MVC.Controllers;
using OnlineGameStore.MVC.Models;
using OnlineGameStore.Tests.Helpers;
using Xunit;

namespace OnlineGameStore.Tests.Controllers
{
    public partial class OrderControllerTests
    {
        [Theory]
        [AutoMoqData]
        public void GetOrders_ReturnsViewResult(
            List<Order> orders,
            FilterOrderViewModel filterOrderViewModel,
            [Frozen] Mock<IOrderService> mockOrderService,
            OrderController sut)
        {
            // Arrange
            mockOrderService.Setup(x => x.GetOrders(It.IsAny<FilterOrderModel>()))
                .Returns(orders);

            // Act
            var result = sut.GetOrders(filterOrderViewModel);

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<OrderListViewModel>()
                .Which.Orders.Should().HaveSameCount(orders);

            mockOrderService.Verify(x => x.GetOrders(It.IsAny<FilterOrderModel>()),
                Times.Once);
        }
    }
}