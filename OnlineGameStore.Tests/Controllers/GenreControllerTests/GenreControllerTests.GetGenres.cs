using System.Collections.Generic;
using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Services.Contracts;
using OnlineGameStore.MVC.Controllers;
using OnlineGameStore.MVC.Models;
using OnlineGameStore.Tests.Helpers;
using Xunit;

namespace OnlineGameStore.Tests.Controllers
{
    public partial class GenreControllerTests
    {
        [Theory]
        [AutoMoqData]
        public void GetGames_ReturnsViewResult(
            IEnumerable<Genre> genres,
            [Frozen] Mock<IGenreService> mockGenreService,
            GenreController sut)
        {
            // Arrange
            mockGenreService.Setup(x => x.GetAllParentGenres())
                .Returns(genres);

            // Act
            var result = sut.GetGenres();

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<IEnumerable<GenreViewModel>>()
                .Which.Should().HaveSameCount(genres);

            mockGenreService.Verify(x => x.GetAllParentGenres(), Times.Once);
        }
    }
}