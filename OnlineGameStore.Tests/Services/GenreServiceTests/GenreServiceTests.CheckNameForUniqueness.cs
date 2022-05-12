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
        public void CheckNameForUniqueness_ReturnsTrue_WhenGenreIsNotNullAndIdIsNotSame(
            Genre genre,
            int id,
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
            var actualResult = sut.CheckNameForUnique(id, genre.Name);

            // Assert
            actualResult.Should().BeTrue();

            mockUnitOfWork.Verify(x => x.Genres.GetSingle(
                    It.IsAny<Expression<Func<Genre, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<string[]>()),
                Times.Once);
        }

        [Theory]
        [InlineAutoMoqData(null)]
        public void CheckNameForUniqueness_ReturnsFalse_WhenGenreIsNull(
            Genre genre,
            int id,
            string name,
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
            var actualResult = sut.CheckNameForUnique(id, name);

            // Assert
            actualResult.Should().BeFalse();

            mockUnitOfWork.Verify(x => x.Genres.GetSingle(
                    It.IsAny<Expression<Func<Genre, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<string[]>()),
                Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public void CheckNameForUniqueness_ReturnsFalse_WhenIdIsSame(
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
            var actualResult = sut.CheckNameForUnique(genre.Id, genre.Name);

            // Assert
            actualResult.Should().BeFalse();

            mockUnitOfWork.Verify(x => x.Genres.GetSingle(
                    It.IsAny<Expression<Func<Genre, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<string[]>()),
                Times.Once);
        }
    }
}