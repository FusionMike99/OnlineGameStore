using System;
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
        public void GenreService_GetGenreById_ReturnsGenre(
            Genre genre,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            GenreService sut)
        {
            // Arrange
            mockUnitOfWork
                .Setup(m => m.Genres.GetSingle(
                    It.IsAny<Expression<Func<Genre, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<string[]>()))
                .Returns(genre);

            // Act
            var actualGenre = sut.GetGenreById(genre.Id);

            // Assert
            actualGenre.Should().BeEquivalentTo(genre);

            mockUnitOfWork.Verify(x => x.Genres.GetSingle(
                    It.IsAny<Expression<Func<Genre, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<string[]>()),
                Times.Once);
        }
    }
}