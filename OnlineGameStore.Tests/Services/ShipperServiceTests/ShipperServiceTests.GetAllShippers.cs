using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Entities.Northwind;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Services;
using OnlineGameStore.Tests.Helpers;
using Xunit;

namespace OnlineGameStore.Tests.Services.ShipperServiceTests
{
    public class ShipperServiceTests
    {
        [Theory]
        [AutoMoqData]
        public void ShipperService_GetAllShippers_ReturnsShippers(
            List<NorthwindShipper> shippers,
            [Frozen] Mock<INorthwindUnitOfWork> mockNorthwindUnitOfWork,
            ShipperService sut)
        {
            // Arrange
            mockNorthwindUnitOfWork
                .Setup(m => m.Shippers.GetMany(It.IsAny<Expression<Func<NorthwindShipper, bool>>>(),
                    It.IsAny<Func<IQueryable<NorthwindShipper>, IOrderedQueryable<NorthwindShipper>>>(),
                    It.IsAny<int?>(), It.IsAny<int?>()))
                .Returns(shippers);

            // Act
            var actualShippers = sut.GetAllShippers();

            // Assert
            actualShippers.Should().BeEquivalentTo(shippers);

            mockNorthwindUnitOfWork.Verify(x => x.Shippers.GetMany(
                    It.IsAny<Expression<Func<NorthwindShipper, bool>>>(),
                    It.IsAny<Func<IQueryable<NorthwindShipper>, IOrderedQueryable<NorthwindShipper>>>(),
                    It.IsAny<int?>(), It.IsAny<int?>()),
                Times.Once);
        }
    }
}