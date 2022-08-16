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
        public async Task GenreService_GetGenreById_ReturnsGenre(
            GenreModel genre,
            [Frozen] Mock<IGenreRepository> genreRepositoryMock,
            GenreService sut)
        {
            // Arrange
            genreRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(genre);

            // Act
            var actualGenre = await sut.GetGenreByIdAsync(genre.Id);

            // Assert
            actualGenre.Should().BeEquivalentTo(genre);

            genreRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }
    }
}