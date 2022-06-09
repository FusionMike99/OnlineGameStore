﻿using System.Collections.Generic;
using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Models;
using OnlineGameStore.BLL.Services.Contracts;
using OnlineGameStore.MVC.Controllers;
using OnlineGameStore.MVC.Models;
using OnlineGameStore.Tests.Helpers;
using Xunit;

namespace OnlineGameStore.Tests.Controllers
{
    public partial class GameControllerTests
    {
        [Theory]
        [AutoMoqData]
        public void GetGames_ReturnsViewResult(
            IEnumerable<Game> games,
            SortFilterGameViewModel sortFilterGameViewModel,
            [Frozen] Mock<IGameService> mockGameService,
            GameController sut)
        {
            // Arrange
            mockGameService.Setup(x => x.GetAllGames(It.IsAny<SortFilterGameModel>(),
                    It.IsAny<PageModel>()))
                .Returns(games);

            // Act
            var result = sut.GetGames(sortFilterGameViewModel);

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<GameListViewModel>()
                .Which.Games.Should().HaveSameCount(games);

            mockGameService.Verify(x => x.GetAllGames(It.IsAny<SortFilterGameModel>(),
                It.IsAny<PageModel>()),
                Times.Once);
        }
    }
}