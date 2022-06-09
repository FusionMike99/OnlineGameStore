﻿using AutoFixture.Xunit2;
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
        public void RemoveFromBasket_ReturnsRedirectToActionResult_WhenIdHasValue(
            int id,
            [Frozen] Mock<IOrderService> mockOrderService,
            OrderController sut)
        {
            // Arrange
            mockOrderService.Setup(x => x.RemoveFromOrder(It.IsAny<int>(),
                It.IsAny<int>()));

            // Act
            var result = sut.RemoveFromBasket(id);

            // Assert
            result.Should().BeOfType<RedirectToActionResult>()
                .Subject.ActionName.Should().BeEquivalentTo(nameof(sut.GetBasket));

            mockOrderService.Verify(x => x.RemoveFromOrder(It.IsAny<int>(),
                It.IsAny<int>()),
                Times.Once);
        }

        [Theory]
        [InlineAutoMoqData(null)]
        public void RemoveFromBasket_ReturnsBadRequestResult_WhenIdHasNotValue(
            int? id,
            OrderController sut)
        {
            // Act
            var result = sut.RemoveFromBasket(id);

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }
    }
}