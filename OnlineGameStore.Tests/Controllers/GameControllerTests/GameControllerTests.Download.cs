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
    public partial class GameControllerTests
    {
        [Theory]
        [AutoMoqData]
        public async Task Download_ReturnsFileContentResult_WhenGameKeyHasValue(
            GameModel game,
            [Frozen] Mock<IGameService> mockGameService,
            GameController sut)
        {
            // Arrange
            mockGameService.Setup(x => x.GetGameByKey(It.IsAny<string>(),
                    It.IsAny<bool>()))
                .ReturnsAsync(game);

            // Act
            var result = await sut.Download(game.Key);

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
        public async Task Download_ReturnsBadRequestResult_WhenGameKeyHasNotValue(
            string gameKey,
            GameController sut)
        {
            // Act
            var result = await sut.Download(gameKey);

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Theory]
        [InlineAutoMoqData(null)]
        public async Task Download_ReturnsNotFoundResult_WhenGameIsNotFound(
            GameModel game,
            string gameKey,
            [Frozen] Mock<IGameService> mockGameService,
            GameController sut)
        {
            // Arrange
            mockGameService.Setup(x => x.GetGameByKey(It.IsAny<string>(),
                    It.IsAny<bool>()))
                .ReturnsAsync(game);

            // Act
            var result = await sut.Download(gameKey);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            mockGameService.Verify(x => x.GetGameByKey(It.IsAny<string>(),
                It.IsAny<bool>()),
                Times.Once);
        }
    }
}