using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Entities.Northwind;
using OnlineGameStore.BLL.Models;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Repositories.GameStore;
using OnlineGameStore.BLL.Repositories.Northwind;
using OnlineGameStore.BLL.Services;
using OnlineGameStore.Tests.Helpers;
using Xunit;

namespace OnlineGameStore.Tests.Services
{
    public partial class OrderServiceTests
    {
        [Theory]
        [AutoMoqData]
        public void OrderService_GetOrder_ReturnsOrders(
            List<Order> orders,
            List<NorthwindOrder> northwindOrders,
            List<NorthwindOrderDetail> orderDetails,
            NorthwindShipper shipper,
            FilterOrderModel filterOrderModel,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            [Frozen] Mock<INorthwindUnitOfWork> mockNorthwindUnitOfWork,
            OrderService sut)
        {
            // Arrange
            mockUnitOfWork.Setup(m => m.Orders.GetMany(It.IsAny<Expression<Func<Order, bool>>>(),
                    It.IsAny<bool>(), It.IsAny<Func<IQueryable<Order>,IOrderedQueryable<Order>>>(),
                    It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<string[]>()))
                .Returns(orders);

            mockNorthwindUnitOfWork.Setup(m => m.Orders.GetMany(
                    It.IsAny<Expression<Func<NorthwindOrder, bool>>>(),
                    It.IsAny<Func<IQueryable<NorthwindOrder>, IOrderedQueryable<NorthwindOrder>>>(),
                    It.IsAny<int?>(), It.IsAny<int?>()))
                .Returns(northwindOrders);

            mockNorthwindUnitOfWork.Setup(m => m.OrderDetails.GetMany(
                    It.IsAny<Expression<Func<NorthwindOrderDetail, bool>>>(),
                    It.IsAny<Func<IQueryable<NorthwindOrderDetail>, IOrderedQueryable<NorthwindOrderDetail>>>(),
                    It.IsAny<int?>(), It.IsAny<int?>()))
                .Returns(orderDetails);

            mockNorthwindUnitOfWork.Setup(m => m.Shippers.GetFirst(
                    It.IsAny<Expression<Func<NorthwindShipper, bool>>>()))
                .Returns(shipper);

            var expectedNorthwindOrder = northwindOrders.Count;
            var expectedCount = orders.Count + expectedNorthwindOrder;

            // Act
            var actualOrder = sut.GetOrders(filterOrderModel);

            // Assert
            actualOrder.Should().HaveCount(expectedCount);

            mockUnitOfWork.Verify(m => m.Orders.GetMany(It.IsAny<Expression<Func<Order, bool>>>(),
                    It.IsAny<bool>(), It.IsAny<Func<IQueryable<Order>,IOrderedQueryable<Order>>>(),
                    It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<string[]>()));

            mockNorthwindUnitOfWork.Verify(m => m.Orders.GetMany(
                    It.IsAny<Expression<Func<NorthwindOrder, bool>>>(),
                    It.IsAny<Func<IQueryable<NorthwindOrder>, IOrderedQueryable<NorthwindOrder>>>(),
                    It.IsAny<int?>(), It.IsAny<int?>()),
                Times.Once);

            mockNorthwindUnitOfWork.Verify(m => m.OrderDetails.GetMany(
                    It.IsAny<Expression<Func<NorthwindOrderDetail, bool>>>(),
                    It.IsAny<Func<IQueryable<NorthwindOrderDetail>, IOrderedQueryable<NorthwindOrderDetail>>>(),
                    It.IsAny<int?>(), It.IsAny<int?>()),
                Times.Exactly(expectedNorthwindOrder));

            mockNorthwindUnitOfWork.Verify(m => m.Shippers.GetFirst(
                    It.IsAny<Expression<Func<NorthwindShipper, bool>>>()),
                Times.Exactly(expectedNorthwindOrder));
        }
    }
}