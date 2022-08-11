using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using OnlineGameStore.BLL.Models.General;
using OnlineGameStore.MVC.Strategies.PaymentMethods;
using OnlineGameStore.Tests.Helpers;
using Xunit;

namespace OnlineGameStore.Tests.Strategies.PaymentMethods
{
    public class VisaPaymentMethodStrategyTests
    {
        [Theory]
        [AutoMoqData]
        public void ProcessPayment_ReturnsViewResult(
            OrderModel order,
            VisaPaymentMethodStrategy sut)
        {
            // Act
            var result = sut.PaymentProcess(order);

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.ViewData.Should().ContainKey("OrderId")
                .WhoseValue.Should().BeEquivalentTo(order.Id);
        }
    }
}