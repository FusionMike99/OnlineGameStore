﻿using System;
using System.Collections.Generic;
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
        public async Task GenreService_GetAllWithoutGenre_ReturnsGenres(
            Guid genreId,
            List<GenreModel> genres,
            [Frozen] Mock<IGenreRepository> genreRepositoryMock,
            GenreService sut)
        {
            // Arrange
            genreRepositoryMock.Setup(x => x.GetWithoutGenre(It.IsAny<Guid>(), It.IsAny<bool>(),
                    It.IsAny<string[]>()))
                .ReturnsAsync(genres);

            // Act
            var actualGenres = await sut.GetAllWithoutGenre(genreId);

            // Assert
            actualGenres.Should().BeEquivalentTo(genres);

            genreRepositoryMock.Verify(x => x.GetWithoutGenre(It.IsAny<Guid>(), It.IsAny<bool>(),
                It.IsAny<string[]>()), Times.Once);
        }
    }
}