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
        public async Task GenreService_CreateGenre_ReturnsGenre(
            GenreModel genre,
            [Frozen] Mock<IGenreRepository> genreRepositoryMock,
            GenreService sut)
        {
            // Arrange
            genreRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<GenreModel>()));

            // Act
            var actualGenre = await sut.CreateGenreAsync(genre);

            // Assert
            actualGenre.Should().BeEquivalentTo(genre);

            genreRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<GenreModel>()), Times.Once);
        }
    }
}