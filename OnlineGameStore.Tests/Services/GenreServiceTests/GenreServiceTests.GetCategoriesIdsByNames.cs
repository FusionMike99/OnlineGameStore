using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Repositories.GameStore;
using OnlineGameStore.BLL.Services;
using OnlineGameStore.Tests.Helpers;
using Xunit;

namespace OnlineGameStore.Tests.Services
{
    public partial class GenreServiceTests
    {
        [Theory]
        [AutoMoqData]
        public void GenreService_GetGenresIdsByNames_ReturnsIds(
            List<Genre> genres,
            string[] names,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            GenreService sut)
        {
            // Arrange
            mockUnitOfWork
                .Setup(m => m.Genres.GetMany(
                    It.IsAny<Expression<Func<Genre, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<Func<IQueryable<Genre>,IOrderedQueryable<Genre>>>(),
                    It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<string[]>()))
                .Returns(genres);

            var expectedIds = genres.Select(x => x.Id);

            // Act
            var actualGenres = sut.GetGenresIdsByNames(names);

            // Assert
            actualGenres.Should().BeEquivalentTo(expectedIds);

            mockUnitOfWork.Verify(x => x.Genres.GetMany(
                    It.IsAny<Expression<Func<Genre, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<Func<IQueryable<Genre>,IOrderedQueryable<Genre>>>(),
                    It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<string[]>()),
                Times.Once);
        }
    }
}