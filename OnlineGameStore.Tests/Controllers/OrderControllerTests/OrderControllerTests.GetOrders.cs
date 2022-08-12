using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineGameStore.BLL.Models;
using OnlineGameStore.BLL.Models.General;
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
        public async Task GetOrders_ReturnsViewResult(
            List<OrderModel> orders,
            FilterOrderViewModel filterOrderViewModel,
            [Frozen] Mock<IOrderService> mockOrderService,
            OrderController sut)
        {
            // Arrange
            mockOrderService.Setup(x => x.GetOrdersAsync(It.IsAny<FilterOrderModel>()))
                .ReturnsAsync(orders);

            // Act
            var result = await sut.GetOrders(filterOrderViewModel);

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<OrderListViewModel>()
                .Which.Orders.Should().HaveSameCount(orders);

            mockOrderService.Verify(x => x.GetOrdersAsync(It.IsAny<FilterOrderModel>()),
                Times.Once);
        }
    }
}