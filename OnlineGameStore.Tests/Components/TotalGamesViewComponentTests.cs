﻿using System.Collections.Generic;
using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using OnlineGameStore.BLL.Entities;
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
        public void Invoke_ReturnsViewViewComponentResult(
            List<Game> games,
            [Frozen] Mock<IGameService> mockGameService,
            TotalGamesViewComponent sut)
        {
            // Arrange
            mockGameService.Setup(x => x.GetAllGames())
                .Returns(games);

            // Act
            var result = sut.Invoke();

            // Assert
            result.Should().BeOfType<ViewViewComponentResult>()
                .Which.ViewData.Model.Should().BeAssignableTo<int>()
                .Which.Should().Be(games.Count);

            mockGameService.Verify(x => x.GetAllGames(), Times.Once);
        }
    }
}