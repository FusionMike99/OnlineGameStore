﻿using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Services;
using OnlineGameStore.Tests.Helpers;
using System;
using Xunit;

namespace OnlineGameStore.Tests.Services
{
    public partial class GenreServiceTests
    {
        [Theory]
        [AutoMoqData]
        public void GenreService_DeleteGenre_DeletesGenre(
            Genre genre,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            GenreService sut)
        {
            // Arrange
            mockUnitOfWork
                .Setup(m => m.Genres.GetSingle(
                    It.IsAny<Func<Genre, bool>>(),
                    It.IsAny<bool>()))
                .Returns(genre);

            mockUnitOfWork.Setup(x => x.Genres.Delete(It.IsAny<Genre>()));

            // Act
            sut.DeleteGenre(genre.Id);

            // Assert
            mockUnitOfWork.Verify(x => x.Genres.GetSingle(
                It.IsAny<Func<Genre, bool>>(),
                It.IsAny<bool>()),
                Times.Once);
            mockUnitOfWork.Verify(x => x.Genres.Delete(
                It.Is<Genre>(g => g.Name == genre.Name && g.Id == genre.Id)),
                Times.Once);
            mockUnitOfWork.Verify(x => x.Commit(), Times.Once);
        }

        [Theory]
        [InlineAutoMoqData(null)]
        public void GenreService_DeleteGenre_ThrowsInvalidOperationExceptionWithNullEntity(
            Genre genre,
            int genreId,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            GenreService sut)
        {
            // Arrange
            mockUnitOfWork
                .Setup(m => m.Genres.GetSingle(
                    It.IsAny<Func<Genre, bool>>(),
                    It.IsAny<bool>()))
                .Returns(genre);

            // Act
            Action actual = () => sut.DeleteGenre(genreId);

            // Assert
            actual.Should().Throw<InvalidOperationException>();

            mockUnitOfWork.Verify(x => x.Genres.GetSingle(
                It.IsAny<Func<Genre, bool>>(),
                It.IsAny<bool>(),
                It.IsAny<string[]>()),
                Times.Once);
        }
    }
}
