﻿using System.Collections.Generic;
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
        public async Task GenreService_GetAllParentGenres_ReturnsGenres(
            List<GenreModel> genres,
            [Frozen] Mock<IGenreRepository> genreRepositoryMock,
            GenreService sut)
        {
            // Arrange
            genreRepositoryMock.Setup(x => x.GetParentGenresAsync())
                .ReturnsAsync(genres);

            // Act
            var actualGenres = await sut.GetAllParentGenresAsync();

            // Assert
            actualGenres.Should().BeEquivalentTo(genres);

            genreRepositoryMock.Verify(x => x.GetParentGenresAsync(), Times.Once);
        }
    }
}