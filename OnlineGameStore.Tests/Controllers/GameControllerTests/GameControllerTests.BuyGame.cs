using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Services.Contracts;
using OnlineGameStore.MVC.Services.Contracts;
using OnlineGameStore.MVC.Controllers;
using OnlineGameStore.Tests.Helpers;
using Xunit;

namespace OnlineGameStore.Tests.Controllers
{
    public partial class GameControllerTests
    {
        [Theory]
        [AutoMoqData]
        public void AddItem_ReturnsRedirectToActionResult_WhenGameIsFound(
            Game game,
            [Frozen] Mock<IGameService> mockGameService,
            [Frozen] Mock<ICartService> mockCartService,
            GameController sut)
        {
            // Arrange
            mockGameService.Setup(x => x.GetGameByKey(It.IsAny<string>()))
                .Returns(game);

            mockCartService.Setup(x => x.AddItem(It.IsAny<Game>(), It.IsAny<short>()));

            // Act
            var result = sut.BuyGame(game.Key);

            // Assert
            result.Should().BeOfType<RedirectToActionResult>()
                .Subject.ActionName.Should().BeEquivalentTo(nameof(sut.GetGameByKey));

            mockGameService.Verify(x => x.GetGameByKey(It.IsAny<string>()), Times.Once);

            mockCartService.Verify(x => x.AddItem(It.IsAny<Game>(), It.IsAny<short>()), Times.Once);
        }

        [Theory]
        [InlineAutoMoqData(null)]
        public void AddItem_ReturnsRedirectToActionResult_WhenGameIsNotFound(
            Game game,
            string gameKey,
            [Frozen] Mock<IGameService> mockGameService,
            GameController sut)
        {
            // Arrange
            mockGameService.Setup(x => x.GetGameByKey(It.IsAny<string>()))
                .Returns(game);

            // Act
            var result = sut.BuyGame(gameKey);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>()
                .Which.Value.Should().BeOfType<string>();

            mockGameService.Verify(x => x.GetGameByKey(It.IsAny<string>()), Times.Once);
        }
    }
}
