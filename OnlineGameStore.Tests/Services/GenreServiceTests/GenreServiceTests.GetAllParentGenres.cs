using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Services;
using OnlineGameStore.Tests.Helpers;
using Xunit;

namespace OnlineGameStore.Tests.Services
{
    public partial class GenreServiceTests
    {
        [Theory]
        [AutoMoqData]
        public void GenreService_GetAllGenres_ReturnsGenres(
            IEnumerable<Genre> genres,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            GenreService sut)
        {
            // Arrange
            mockUnitOfWork
                .Setup(m => m.Genres.GetMany(
                    It.IsAny<Expression<Func<Genre, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<string[]>()))
                .Returns(genres);

            // Act
            var actualGenres = sut.GetAllParentGenres();

            // Assert
            actualGenres.Should().BeEquivalentTo(genres);

            mockUnitOfWork.Verify(x => x.Genres.GetMany(
                    It.IsAny<Expression<Func<Genre, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<string[]>()),
                Times.Once);
        }
    }
}