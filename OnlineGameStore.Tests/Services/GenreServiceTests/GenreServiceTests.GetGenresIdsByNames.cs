using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using OnlineGameStore.BLL.Entities.Northwind;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Repositories.Northwind;
using OnlineGameStore.BLL.Services;
using OnlineGameStore.Tests.Helpers;
using Xunit;

namespace OnlineGameStore.Tests.Services
{
    public partial class GenreServiceTests
    {
        [Theory]
        [AutoMoqData]
        public void GenreService_GetCategoriesIdsByNames_ReturnsIds(
            List<NorthwindCategory> categories,
            string[] names,
            [Frozen] Mock<INorthwindUnitOfWork> mockNorthwindUnitOfWork,
            GenreService sut)
        {
            // Arrange
            mockNorthwindUnitOfWork
                .Setup(m => m.Categories.GetMany(It.IsAny<Expression<Func<NorthwindCategory, bool>>>(),
                    It.IsAny<Func<IQueryable<NorthwindCategory>, IOrderedQueryable<NorthwindCategory>>>(),
                    It.IsAny<int?>(), It.IsAny<int?>()))
                .Returns(categories);

            var expectedIds = categories.Select(x => x.CategoryId);

            // Act
            var actualGenres = sut.GetCategoriesIdsByNames(names);

            // Assert
            actualGenres.Should().BeEquivalentTo(expectedIds);

            mockNorthwindUnitOfWork.Verify(x => x.Categories.GetMany(
                    It.IsAny<Expression<Func<NorthwindCategory, bool>>>(),
                    It.IsAny<Func<IQueryable<NorthwindCategory>, IOrderedQueryable<NorthwindCategory>>>(),
                    It.IsAny<int?>(), It.IsAny<int?>()),
                Times.Once);
        }
    }
}