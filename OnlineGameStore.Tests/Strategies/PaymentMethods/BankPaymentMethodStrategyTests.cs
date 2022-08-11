using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using OnlineGameStore.BLL.Models.General;
using OnlineGameStore.MVC.Strategies.PaymentMethods;
using OnlineGameStore.Tests.Helpers;
using Xunit;

namespace OnlineGameStore.Tests.Strategies.PaymentMethods
{
    public class BankPaymentMethodStrategyTests
    {
        [Theory]
        [AutoMoqData]
        public void ProcessPayment_ReturnsFileContentResult(
            OrderModel order,
            BankPaymentMethodStrategy sut)
        {
            // Act
            var result = sut.PaymentProcess(order);

            // Assert
            result.Should().BeOfType<FileContentResult>();
        }
    }
}