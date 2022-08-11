using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
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
        public async Task GenreService_GetCategoriesIdsByNames_ReturnsIds(
            List<string> categories,
            string[] names,
            [Frozen] Mock<IGenreRepository> genreRepositoryMock,
            GenreService sut)
        {
            // Arrange
            genreRepositoryMock.Setup(x => x.GetCategoryIdsByNamesAsync(names))
                .ReturnsAsync(categories);

            // Act
            var actualGenres = await sut.GetCategoriesIdsByNames(names);

            // Assert
            actualGenres.Should().BeEquivalentTo(categories);

            genreRepositoryMock.Verify(x => x.GetCategoryIdsByNamesAsync(names), Times.Once);
        }
    }
}