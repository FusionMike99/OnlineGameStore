using System;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineGameStore.BLL.Services.Interfaces;
using OnlineGameStore.DomainModels.Models.General;
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
        public async Task GetBasket_ReturnsViewResult(
            OrderModel order,
            [Frozen] Mock<ICustomerIdAccessor> mockCustomerIdAccessor,
            [Frozen] Mock<IOrderService> mockOrderService,
            OrderController sut)
        {
            // Arrange
            mockCustomerIdAccessor.Setup(x => x.GetCustomerId())
                .Returns(order.CustomerId);

            mockOrderService.Setup(x => x.GetOpenOrInProcessOrderAsync(It.IsAny<Guid>()))
                .ReturnsAsync(order);

            // Act
            var result = await sut.GetBasket();

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<OrderViewModel>()
                .Which.Id.Should().Be(order.Id);

            mockCustomerIdAccessor.Verify(x => x.GetCustomerId(), Times.Once);

            mockOrderService.Verify(x => x.GetOpenOrInProcessOrderAsync(It.IsAny<Guid>()),
                Times.Once);
        }
    }
}