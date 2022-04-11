﻿using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Services;
using OnlineGameStore.Tests.Helpers;
using System;
using System.Linq.Expressions;
using Xunit;

namespace OnlineGameStore.Tests.Services
{
    public partial class GameServiceTests
    {
        [Theory]
        [AutoMoqData]
        public void GameService_GetGameByKey_ReturnsGame(
            string gameKey,
            Game game,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            GameService sut)
        {
            // Arrange
            mockUnitOfWork
                .Setup(m => m.Games.GetSingle(
                    It.IsAny<Expression<Func<Game, bool>>>(),
                    It.IsAny<string[]>()))
                .Returns(game);

            // Act
            var actualGame = sut.GetGameByKey(gameKey);

            // Assert
            actualGame.Should().BeEquivalentTo(game);

            mockUnitOfWork.Verify(x => x.Games.GetSingle(
                It.IsAny<Expression<Func<Game, bool>>>(),
                It.IsAny<string[]>()),
                Times.Once);
        }
    }
}