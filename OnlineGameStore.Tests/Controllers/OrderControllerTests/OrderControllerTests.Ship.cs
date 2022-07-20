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
    public partial class OrderControllerTests
    {
        [Theory]
        [AutoMoqData]
        public void Ship_Get_ReturnsViewResult(
            Order order,
            [Frozen] Mock<IOrderService> mockOrderService,
            OrderController sut)
        {
            // Arrange
            mockOrderService.Setup(x => x.GetOrderById(It.IsAny<int>()))
                .Returns(order);

            // Act
            var result = sut.Ship(order.Id);

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<ShipOrderViewModel>()
                .Which.Id.Should().Be(order.Id);
            
            mockOrderService.Verify(x => x.GetOrderById(It.IsAny<int>()), Times.Once);
        }

        [Theory]
        [InlineAutoMoqData(null)]
        public void Ship_Get_ReturnsNotFoundResult_WhenOrderIsNotFound(
            Order order,
            int orderId,
            [Frozen] Mock<IOrderService> mockOrderService,
            OrderController sut)
        {
            // Arrange
            mockOrderService.Setup(x => x.GetOrderById(It.IsAny<int>()))
                .Returns(order);

            // Act
            var result = sut.Ship(orderId);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            mockOrderService.Verify(x => x.GetOrderById(It.IsAny<int>()), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public void Ship_Post_ReturnsRedirectToActionResult_WhenOrderIsValid(
            Order order,
            [Frozen] Mock<IOrderService> mockOrderService,
            OrderController sut)
        {
            // Arrange
            mockOrderService.Setup(x => x.EditOrder(It.IsAny<Order>()))
                .Returns(order);

            var shipOrderViewModel = new ShipOrderViewModel
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                ShipVia = order.ShipVia
            };

            // Act
            var result = sut.Ship(shipOrderViewModel.Id, shipOrderViewModel);

            // Assert
            result.Should().BeOfType<RedirectToActionResult>()
                .Subject.ActionName.Should().BeEquivalentTo(nameof(sut.Make));

            mockOrderService.Verify(x => x.EditOrder(It.IsAny<Order>()), Times.Once);
        }
        
        [Theory]
        [AutoMoqData]
        public void Ship_Post_ReturnsViewResult_WhenOrderIsInvalid(
            ShipOrderViewModel shipOrderViewModel,
            OrderController sut)
        {
            // Arrange
            sut.ModelState.AddModelError("Name", "Required");

            // Act
            var result = sut.Ship(shipOrderViewModel.Id, shipOrderViewModel);

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<ShipOrderViewModel>()
                .Which.Id.Should().Be(shipOrderViewModel.Id);
        }
    }
}