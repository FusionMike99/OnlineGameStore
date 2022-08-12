using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using OnlineGameStore.BLL.Models;
using OnlineGameStore.BLL.Services.Contracts;
using OnlineGameStore.MVC.Components;
using OnlineGameStore.Tests.Helpers;
using Xunit;

namespace OnlineGameStore.Tests.Components
{
    public class TotalGamesViewComponentTests
    {
        [Theory]
        [AutoMoqData]
        public async Task Invoke_ReturnsViewViewComponentResult(
            int gamesCount,
            [Frozen] Mock<IGameService> mockGameService,
            TotalGamesViewComponent sut)
        {
            // Arrange
            mockGameService.Setup(x => x.GetGamesNumberAsync(It.IsAny<SortFilterGameModel>()))
                .ReturnsAsync(gamesCount);

            // Act
            var result = await sut.InvokeAsync();

            // Assert
            result.Should().BeOfType<ViewViewComponentResult>()
                .Which.ViewData.Model.Should().BeAssignableTo<int>()
                .Which.Should().Be(gamesCount);

            mockGameService.Verify(x => x.GetGamesNumberAsync(It.IsAny<SortFilterGameModel>()),
                Times.Once);
        }
    }
}