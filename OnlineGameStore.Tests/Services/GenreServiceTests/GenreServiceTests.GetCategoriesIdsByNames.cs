﻿using System.Collections.Generic;
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
        public async Task GenreService_GetGenresIdsByNames_ReturnsIds(
            List<string> genres,
            string[] names,
            [Frozen] Mock<IGenreRepository> genreRepositoryMock,
            GenreService sut)
        {
            // Arrange
            genreRepositoryMock.Setup(x => x.GetGenreIdsByNamesAsync(names))
                .ReturnsAsync(genres);

            // Act
            var actualGenres = await sut.GetGenresIdsByNames(names);

            // Assert
            actualGenres.Should().BeEquivalentTo(genres);

            genreRepositoryMock.Verify(x => x.GetGenreIdsByNamesAsync(names), Times.Once);
        }
    }
}