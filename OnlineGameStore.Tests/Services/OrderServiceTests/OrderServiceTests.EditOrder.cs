using System;
using System.Linq.Expressions;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Services;
using OnlineGameStore.Tests.Helpers;
using Xunit;

namespace OnlineGameStore.Tests.Services
{
    public partial class OrderServiceTests
    {
        [Theory]
        [AutoMoqData]
        public void OrderService_EditOrder_ReturnsOrder(
            Order order,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            OrderService sut)
        {
            // Arrange
            mockUnitOfWork.Setup(x => x.Orders.GetSingle(It.IsAny<Expression<Func<Order, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<string[]>()))
                .Returns(order);

            mockUnitOfWork.Setup(x => x.Orders.Update(It.IsAny<Order>(),
                    It.IsAny<Expression<Func<Order, bool>>>()))
                .Returns(order);

            // Act
            var actualOrder = sut.EditOrder(order);

            // Assert
            actualOrder.Should().BeEquivalentTo(order);

            mockUnitOfWork.Verify(x => x.Orders.GetSingle(It.IsAny<Expression<Func<Order, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<string[]>()),
                Times.Once);

            mockUnitOfWork.Verify(x => x.Orders.Update(It.IsAny<Order>(),
                It.IsAny<Expression<Func<Order,bool>>>()), Times.Once);
            mockUnitOfWork.Verify(x => x.Commit(), Times.Once);
        }
    }
}