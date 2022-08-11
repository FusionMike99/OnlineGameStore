using System;
using System.Threading.Tasks;
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
        public async Task Pay_ReturnsRedirectToActionResult(
            Guid orderId,
            [Frozen] Mock<IOrderService> mockOrderService,
            OrderController sut)
        {
            // Arrange
            mockOrderService.Setup(x => x.ChangeStatusToClosed(It.IsAny<Guid>()));

            // Act
            var result = await sut.Pay(orderId);

            // Assert
            var redirectToActionResult = result.Should().BeOfType<RedirectToActionResult>().Subject;
            redirectToActionResult.ActionName.Should().Be(nameof(GameController.GetGames));
            redirectToActionResult.ControllerName.Should().Be("Game");

            mockOrderService.Verify(x => x.ChangeStatusToClosed(It.IsAny<Guid>()), Times.Once);
        }
    }
}