using System;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineGameStore.BLL.Models.General;
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
        public async Task AddItem_ReturnsRedirectToActionResult_WhenGameIsFound(
            GameModel game,
            [Frozen] Mock<IGameService> mockGameService,
            [Frozen] Mock<IOrderService> mockOrderService,
            OrderController sut)
        {
            // Arrange
            mockGameService.Setup(x => x.GetGameByKeyAsync(It.IsAny<string>(),
                    It.IsAny<bool>()))
                .ReturnsAsync(game);

            mockOrderService.Setup(x => x.AddToOpenOrderAsync(It.IsAny<Guid>(),
                It.IsAny<GameModel>(), It.IsAny<short>()));

            // Act
            var result = await sut.BuyProduct(game.Key);

            // Assert
            result.Should().BeOfType<RedirectToActionResult>()
                .Subject.ActionName.Should().BeEquivalentTo(nameof(sut.GetBasket));

            mockGameService.Verify(x => x.GetGameByKeyAsync(It.IsAny<string>(),
                It.IsAny<bool>()),
                Times.Once);

            mockOrderService.Verify(x => x.AddToOpenOrderAsync(It.IsAny<Guid>(),
                    It.IsAny<GameModel>(), It.IsAny<short>()),
                Times.Once);
        }

        [Theory]
        [InlineAutoMoqData(null)]
        public async Task AddItem_ReturnsNotFoundResult_WhenGameIsNotFound(
            GameModel game,
            string gameKey,
            [Frozen] Mock<IGameService> mockGameService,
            OrderController sut)
        {
            // Arrange
            mockGameService.Setup(x => x.GetGameByKeyAsync(It.IsAny<string>(), It.IsAny<bool>()))
                .ReturnsAsync(game);

            // Act
            var result = await sut.BuyProduct(gameKey);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            mockGameService.Verify(x => x.GetGameByKeyAsync(It.IsAny<string>(),
                It.IsAny<bool>()), Times.Once);
        }
    }
}