using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Services.Contracts;
using OnlineGameStore.MVC.Controllers;
using OnlineGameStore.MVC.Infrastructure;
using OnlineGameStore.MVC.Models;
using OnlineGameStore.Tests.Helpers;
using Xunit;

namespace OnlineGameStore.Tests.Controllers
{
    public partial class OrderControllerTests
    {
        [Theory]
        [AutoMoqData]
        public void Make_ReturnsViewResult(
            Order order,
            [Frozen] Mock<ICustomerIdAccessor> mockCustomerIdAccessor,
            [Frozen] Mock<IOrderService> mockOrderService,
            OrderController sut)
        {
            // Arrange
            mockCustomerIdAccessor.Setup(x => x.GetCustomerId())
                .Returns(order.CustomerId);

            mockOrderService.Setup(x => x.ChangeStatusToInProcess(It.IsAny<string>()))
                .Returns(order);

            // Act
            var result = sut.Make();

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<OrderViewModel>()
                .Which.Id.Should().Be(order.Id);

            mockCustomerIdAccessor.Verify(x => x.GetCustomerId(), Times.Once);

            mockOrderService.Verify(x => x.ChangeStatusToInProcess(It.IsAny<string>()),
                Times.Once);
        }
    }
}