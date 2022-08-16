using System;
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
    public partial class OrderControllerTests
    {
        [Theory]
        [AutoMoqData]
        public async Task Ship_Get_ReturnsViewResult(
            OrderModel order,
            [Frozen] Mock<IOrderService> mockOrderService,
            OrderController sut)
        {
            // Arrange
            mockOrderService.Setup(x => x.GetOrderByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(order);

            // Act
            var result = await sut.Ship(order.Id);

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<ShipOrderViewModel>()
                .Which.Id.Should().Be(order.Id);
            
            mockOrderService.Verify(x => x.GetOrderByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Theory]
        [InlineAutoMoqData(null)]
        public async Task Ship_Get_ReturnsNotFoundResult_WhenOrderIsNotFound(
            OrderModel order,
            Guid orderId,
            [Frozen] Mock<IOrderService> mockOrderService,
            OrderController sut)
        {
            // Arrange
            mockOrderService.Setup(x => x.GetOrderByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(order);

            // Act
            var result = await sut.Ship(orderId);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            mockOrderService.Verify(x => x.GetOrderByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public async Task Ship_Post_ReturnsRedirectToActionResult_WhenOrderIsValid(
            OrderModel order,
            [Frozen] Mock<IOrderService> mockOrderService,
            OrderController sut)
        {
            // Arrange
            mockOrderService.Setup(x => x.EditOrderAsync(It.IsAny<OrderModel>()))
                .ReturnsAsync(order);

            var shipOrderViewModel = new ShipOrderViewModel
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                ShipVia = order.ShipVia
            };

            // Act
            var result = await sut.Ship(shipOrderViewModel.Id, shipOrderViewModel);

            // Assert
            result.Should().BeOfType<RedirectToActionResult>()
                .Subject.ActionName.Should().BeEquivalentTo(nameof(sut.Make));

            mockOrderService.Verify(x => x.EditOrderAsync(It.IsAny<OrderModel>()), Times.Once);
        }
        
        [Theory]
        [AutoMoqData]
        public async Task Ship_Post_ReturnsViewResult_WhenOrderIsInvalid(
            ShipOrderViewModel shipOrderViewModel,
            OrderController sut)
        {
            // Arrange
            sut.ModelState.AddModelError("Name", "Required");

            // Act
            var result = await sut.Ship(shipOrderViewModel.Id, shipOrderViewModel);

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<ShipOrderViewModel>()
                .Which.Id.Should().Be(shipOrderViewModel.Id);
        }
    }
}