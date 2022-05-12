using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineGameStore.BLL.Services.Contracts;
using OnlineGameStore.MVC.Controllers;
using OnlineGameStore.Tests.Helpers;
using Xunit;

namespace OnlineGameStore.Tests.Controllers
{
    public partial class OrderControllerTests
    {
        [Theory]
        [AutoMoqData]
        public void Pay_ReturnsRedirectToActionResult(
            int orderId,
            [Frozen] Mock<IOrderService> mockOrderService,
            OrderController sut)
        {
            // Arrange
            mockOrderService.Setup(x => x.ChangeStatusToClosed(It.IsAny<int>()));

            // Act
            var result = sut.Pay(orderId);

            // Assert
            var redirectToActionResult = result.Should().BeOfType<RedirectToActionResult>().Subject;
            redirectToActionResult.ActionName.Should().Be(nameof(GameController.GetGames));
            redirectToActionResult.ControllerName.Should().Be("Game");

            mockOrderService.Verify(x => x.ChangeStatusToClosed(It.IsAny<int>()),
                Times.Once);
        }
    }
}