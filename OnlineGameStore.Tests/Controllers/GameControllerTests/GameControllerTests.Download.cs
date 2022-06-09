using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Services.Contracts;
using OnlineGameStore.MVC.Controllers;
using OnlineGameStore.Tests.Helpers;
using Xunit;

namespace OnlineGameStore.Tests.Controllers
{
    public partial class GameControllerTests
    {
        [Theory]
        [AutoMoqData]
        public void Download_ReturnsFileContentResult_WhenGameKeyHasValue(
            Game game,
            [Frozen] Mock<IGameService> mockGameService,
            GameController sut)
        {
            // Arrange
            mockGameService.Setup(x => x.GetGameByKey(It.IsAny<string>(),
                    It.IsAny<bool>()))
                .Returns(game);

            // Act
            var result = sut.Download(game.Key);

            // Assert
            result.Should().BeOfType<FileContentResult>();

            mockGameService.Verify(x => x.GetGameByKey(It.IsAny<string>(),
                It.IsAny<bool>()),
                Times.Once);
        }

        [Theory]
        [InlineAutoMoqData("")]
        [InlineAutoMoqData(" ")]
        [InlineAutoMoqData(null)]
        public void Download_ReturnsBadRequestResult_WhenGameKeyHasNotValue(
            string gameKey,
            GameController sut)
        {
            // Act
            var result = sut.Download(gameKey);

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Theory]
        [InlineAutoMoqData(null)]
        public void Download_ReturnsNotFoundResult_WhenGameIsNotFound(
            Game game,
            string gameKey,
            [Frozen] Mock<IGameService> mockGameService,
            GameController sut)
        {
            // Arrange
            mockGameService.Setup(x => x.GetGameByKey(It.IsAny<string>(),
                    It.IsAny<bool>()))
                .Returns(game);

            // Act
            var result = sut.Download(gameKey);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            mockGameService.Verify(x => x.GetGameByKey(It.IsAny<string>(),
                It.IsAny<bool>()),
                Times.Once);
        }
    }
}