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
    public partial class GameControllerTests
    {
        [Theory]
        [AutoMoqData]
        public async Task Remove_ReturnsRedirectToActionResult_WhenIdHasValue(
            string gameKey,
            [Frozen] Mock<IGameService> mockGameService,
            GameController sut)
        {
            // Arrange
            mockGameService.Setup(x => x.DeleteGame(It.IsAny<string>()));

            // Act
            var result = await sut.Remove(gameKey);

            // Assert
            result.Should().BeOfType<RedirectToActionResult>()
                .Subject.ActionName.Should().BeEquivalentTo(nameof(sut.GetGames));

            mockGameService.Verify(x => x.DeleteGame(It.IsAny<string>()), Times.Once);
        }

        [Theory]
        [InlineAutoMoqData(null)]
        public void Remove_ReturnsBadRequestResult_WhenIdHasNotValue(
            string gameKey,
            GameController sut)
        {
            // Act
            var result = sut.Remove(gameKey);

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }
    }
}