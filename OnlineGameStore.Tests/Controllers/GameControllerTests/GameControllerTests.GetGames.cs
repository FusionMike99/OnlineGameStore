using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Services.Contracts;
using OnlineGameStore.MVC.Controllers;
using OnlineGameStore.MVC.Models;
using OnlineGameStore.Tests.Helpers;
using System.Collections.Generic;
using Xunit;

namespace OnlineGameStore.Tests.Controllers
{
    public partial class GameControllerTests
    {
        [Theory]
        [AutoMoqData]
        public void GetGames_ReturnsJsonResult(
            IEnumerable<Game> games,
            [Frozen] Mock<IGameService> mockGameService,
            GameController sut)
        {
            // Arrange
            mockGameService.Setup(x => x.GetAllGames())
                .Returns(games);

            // Act
            var result = sut.GetGames();

            // Assert
            result.Should().BeOfType<JsonResult>()
                .Which.Value.Should().BeAssignableTo<IEnumerable<GameViewModel>>()
                .Which.Should().HaveSameCount(games);

            mockGameService.Verify(x => x.GetAllGames(), Times.Once);
        }
    }
}
