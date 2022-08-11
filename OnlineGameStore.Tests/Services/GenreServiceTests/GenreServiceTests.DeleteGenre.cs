﻿using System;
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
        public async Task GenreService_DeleteGenre_DeletesGenre(
            GenreModel genre,
            [Frozen] Mock<IGenreRepository> genreRepositoryMock,
            GenreService sut)
        {
            // Arrange
            genreRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>(),
                    It.IsAny<string[]>()))
                .ReturnsAsync(genre);

            genreRepositoryMock.Setup(x => x.DeleteAsync(It.IsAny<GenreModel>()));

            // Act
            await sut.DeleteGenre(genre.Id);

            // Assert
            genreRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>(),
                It.IsAny<string[]>()), Times.Once);
            
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
            genreRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>(),
                    It.IsAny<string[]>()))
                .ReturnsAsync(genre);

            // Act
            Func<Task> actual = async () => await sut.DeleteGenre(genreId);

            // Assert
            await actual.Should().ThrowAsync<InvalidOperationException>();

            genreRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>(),
                It.IsAny<string[]>()), Times.Once);
        }
    }
}