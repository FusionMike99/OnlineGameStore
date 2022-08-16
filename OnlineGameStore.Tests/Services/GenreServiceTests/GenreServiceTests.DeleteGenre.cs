using System;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using OnlineGameStore.BLL.Services;
using OnlineGameStore.DAL.Abstractions.Interfaces;
using OnlineGameStore.DomainModels.Models.General;
using OnlineGameStore.Tests.Helpers;
using Xunit;

namespace OnlineGameStore.Tests.Services
{
    public partial class GenreServiceTests
    {
        [Theory]
        [AutoMoqData]
        public async Task GenreService_DeleteGenre_DeletesGenre(
            GenreModel genre,
            [Frozen] Mock<IGenreRepository> genreRepositoryMock,
            GenreService sut)
        {
            // Arrange
            genreRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(genre);

            genreRepositoryMock.Setup(x => x.DeleteAsync(It.IsAny<GenreModel>()));

            // Act
            await sut.DeleteGenreAsync(genre.Id);

            // Assert
            genreRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
            
            genreRepositoryMock.Verify(x => x.DeleteAsync(
                    It.Is<GenreModel>(g => g.Name == genre.Name && g.Id == genre.Id)),
                Times.Once);
        }

        [Theory]
        [InlineAutoMoqData(null)]
        public async Task GenreService_DeleteGenre_ThrowsInvalidOperationExceptionWithNullEntity(
            GenreModel genre,
            Guid genreId,
            [Frozen] Mock<IGenreRepository> genreRepositoryMock,
            GenreService sut)
        {
            // Arrange
            genreRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(genre);

            // Act
            Func<Task> actual = async () => await sut.DeleteGenreAsync(genreId);

            // Assert
            await actual.Should().ThrowAsync<InvalidOperationException>();

            genreRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }
    }
}