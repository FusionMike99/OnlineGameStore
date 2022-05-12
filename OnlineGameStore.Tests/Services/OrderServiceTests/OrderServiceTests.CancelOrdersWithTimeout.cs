using System;
using System.Linq.Expressions;
using AutoFixture.Xunit2;
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
        public void OrderService_CancelOrderWithTimeout(
            int minutes,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            OrderService sut)
        {
            // Arrange
            var timeout = TimeSpan.FromMinutes(minutes);

            var orders = GetTestOrders(timeout);

            mockUnitOfWork.Setup(x => x.Orders.GetMany(It.IsAny<Expression<Func<Order, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<string[]>()))
                .Returns(orders);

            // Act
            sut.CancelOrdersWithTimeout(timeout);

            // Assert
            mockUnitOfWork.Verify(x => x.Orders.GetMany(It.IsAny<Expression<Func<Order, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<string[]>()),
                Times.Once);

            mockUnitOfWork.Verify(x => x.Orders.Update(It.IsAny<Order>()), Times.Exactly(2));
            mockUnitOfWork.Verify(x => x.Commit(), Times.Once);
        }
    }
}