using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using OnlineGameStore.BLL.Models.General;
using OnlineGameStore.MVC.Models;
using OnlineGameStore.MVC.Strategies.PaymentMethods;
using OnlineGameStore.Tests.Helpers;
using Xunit;

namespace OnlineGameStore.Tests.Strategies.PaymentMethods
{
    public class TerminalIBoxPaymentMethodStrategyTests
    {
        [Theory]
        [AutoMoqData]
        public void ProcessPayment_ReturnsViewResult(
            OrderModel order,
            TerminalIBoxPaymentMethodStrategy sut)
        {
            // Act
            var result = sut.PaymentProcess(order);

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<OrderViewModel>()
                .Which.Id.Should().Be(order.Id.ToString());
        }
    }
}