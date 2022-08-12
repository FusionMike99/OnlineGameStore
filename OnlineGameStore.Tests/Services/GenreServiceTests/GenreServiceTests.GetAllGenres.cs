﻿using System.Collections.Generic;
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
        public async Task GenreService_GetAllGenres_ReturnsGenres(
            List<GenreModel> genres,
            [Frozen] Mock<IGenreRepository> genreRepositoryMock,
            GenreService sut)
        {
            // Arrange
            genreRepositoryMock.Setup(x => x.GetAllAsync(It.IsAny<bool>(), It.IsAny<string[]>()))
                .ReturnsAsync(genres);

            // Act
            var actualGenres = await sut.GetAllGenresAsync();

            // Assert
            actualGenres.Should().BeEquivalentTo(genres);

            genreRepositoryMock.Verify(x => x.GetAllAsync(It.IsAny<bool>(), It.IsAny<string[]>()), Times.Once);
        }
    }
}