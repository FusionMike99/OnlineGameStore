using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using OnlineGameStore.BLL.Entities.Northwind;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Services;
using OnlineGameStore.Tests.Helpers;
using Xunit;

namespace OnlineGameStore.Tests.Services
{
    public partial class PublisherServiceTests
    {
        [Theory]
        [AutoMoqData]
        public void PublisherService_GetSuppliersIdsByNames_ReturnsIds(
            List<NorthwindSupplier> suppliers,
            string[] names,
            [Frozen] Mock<INorthwindUnitOfWork> mockNorthwindUnitOfWork,
            PublisherService sut)
        {
            // Arrange
            mockNorthwindUnitOfWork
                .Setup(m => m.Suppliers.GetMany(It.IsAny<Expression<Func<NorthwindSupplier, bool>>>(),
                    It.IsAny<Func<IQueryable<NorthwindSupplier>, IOrderedQueryable<NorthwindSupplier>>>(),
                    It.IsAny<int?>(), It.IsAny<int?>()))
                .Returns(suppliers);

            var expectedIds = suppliers.Select(x => x.SupplierId);

            // Act
            var actualPublishers = sut.GetSuppliersIdsByNames(names);

            // Assert
            actualPublishers.Should().BeEquivalentTo(expectedIds);

            mockNorthwindUnitOfWork.Verify(x => x.Suppliers.GetMany(
                    It.IsAny<Expression<Func<NorthwindSupplier, bool>>>(),
                    It.IsAny<Func<IQueryable<NorthwindSupplier>, IOrderedQueryable<NorthwindSupplier>>>(),
                    It.IsAny<int?>(), It.IsAny<int?>()),
                Times.Once);
        }
    }
}