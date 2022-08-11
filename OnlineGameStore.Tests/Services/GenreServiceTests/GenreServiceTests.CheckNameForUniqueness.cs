using System;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using OnlineGameStore.BLL.Models.General;
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
        public async Task CheckNameForUniqueness_ReturnsTrue_WhenGenreIsNotNullAndIdIsNotSame(
            GenreModel genre,
            Guid id,
            [Frozen] Mock<IGenreRepository> genreRepositoryMock,
            GenreService sut)
        {
            // Arrange
            genreRepositoryMock.Setup(x => x.GetByNameAsync(It.IsAny<string>(),
                    It.IsAny<bool>(), It.IsAny<string[]>()))
                .ReturnsAsync(genre);

            // Act
            var actualResult = await sut.CheckNameForUnique(id, genre.Name);

            // Assert
            actualResult.Should().BeTrue();

            genreRepositoryMock.Verify(x => x.GetByNameAsync(It.IsAny<string>(),
                It.IsAny<bool>(), It.IsAny<string[]>()), Times.Once);
        }

        [Theory]
        [InlineAutoMoqData(null)]
        public async Task CheckNameForUniqueness_ReturnsFalse_WhenGenreIsNull(
            GenreModel genre,
            Guid id,
            string name,
            [Frozen] Mock<IGenreRepository> genreRepositoryMock,
            GenreService sut)
        {
            // Arrange
            genreRepositoryMock.Setup(x => x.GetByNameAsync(It.IsAny<string>(),
                    It.IsAny<bool>(), It.IsAny<string[]>()))
                .ReturnsAsync(genre);

            // Act
            var actualResult = await sut.CheckNameForUnique(id, name);

            // Assert
            actualResult.Should().BeFalse();

            genreRepositoryMock.Verify(x => x.GetByNameAsync(It.IsAny<string>(),
                It.IsAny<bool>(), It.IsAny<string[]>()), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public async Task CheckNameForUniqueness_ReturnsFalse_WhenIdIsSame(
            GenreModel genre,
            [Frozen] Mock<IGenreRepository> genreRepositoryMock,
            GenreService sut)
        {
            // Arrange
            genreRepositoryMock.Setup(x => x.GetByNameAsync(It.IsAny<string>(),
                    It.IsAny<bool>(), It.IsAny<string[]>()))
                .ReturnsAsync(genre);

            // Act
            var actualResult = await sut.CheckNameForUnique(genre.Id, genre.Name);

            // Assert
            actualResult.Should().BeFalse();

            genreRepositoryMock.Verify(x => x.GetByNameAsync(It.IsAny<string>(),
                It.IsAny<bool>(), It.IsAny<string[]>()), Times.Once);
        }
    }
}