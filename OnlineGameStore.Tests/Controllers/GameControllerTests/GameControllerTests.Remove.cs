﻿using AutoFixture.Xunit2;
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
        public void Remove_ReturnsRedirectToActionResult_WhenIdHasValue(
            int id,
            [Frozen] Mock<IGameService> mockGameService,
            GameController sut)
        {
            // Arrange
            mockGameService.Setup(x => x.DeleteGame(It.IsAny<int>()));

            // Act
            var result = sut.Remove(id);

            // Assert
            result.Should().BeOfType<RedirectToActionResult>()
                .Subject.ActionName.Should().BeEquivalentTo(nameof(sut.GetGames));

            mockGameService.Verify(x => x.DeleteGame(It.IsAny<int>()), Times.Once);
        }

        [Theory]
        [InlineAutoMoqData(null)]
        public void Remove_ReturnsBadRequestResult_WhenIdHasNotValue(
            int? id,
            GameController sut)
        {
            // Act
            var result = sut.Remove(id);

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }
    }
}