using System;
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
    public partial class OrderServiceTests
    {
        [Theory]
        [AutoMoqData]
        public void OrderService_RemoveFromOrder_RemoveOrderDetail_WhenPositionIsExist(
            Game game,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            OrderService sut)
        {
            // Arrange
            var order = GetTestOrder(game);

            var expectedCount = order.OrderDetails.Count - 1;

            mockUnitOfWork.Setup(x => x.Orders.GetSingle(It.IsAny<Expression<Func<Order, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<string[]>()))
                .Returns(order);

            mockUnitOfWork.Setup(x => x.Orders.Update(It.IsAny<Order>(),
                It.IsAny<Expression<Func<Order,bool>>>()));

            var arrangedOrderDetail = order.OrderDetails.First();

            // Act
            sut.RemoveFromOrder(order.CustomerId, arrangedOrderDetail.GameKey);

            // Assert
            order.OrderDetails.Should().HaveCount(expectedCount);

            mockUnitOfWork.Verify(x => x.Orders.GetSingle(It.IsAny<Expression<Func<Order, bool>>>(),
                It.IsAny<bool>(),
                It.IsAny<string[]>()),
                Times.Once());
            
            mockUnitOfWork.Verify(x => x.Orders.Update(It.IsAny<Order>(),
                It.IsAny<Expression<Func<Order,bool>>>()), Times.Once);
            mockUnitOfWork.Verify(x => x.Commit(), Times.Once);
        }
        
        [Theory]
        [AutoMoqData]
        public void OrderService_RemoveFromOrder_DoNothing_WhenPositionIsNotExist(
            Game game,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            OrderService sut)
        {
            // Arrange
            var order = GetTestOrder(game);

            mockUnitOfWork.Setup(x => x.Orders.GetSingle(It.IsAny<Expression<Func<Order, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<string[]>()))
                .Returns(order);

            // Act
            sut.RemoveFromOrder(order.CustomerId, game.Key + 1);

            // Assert
            
            mockUnitOfWork.Verify(x => x.Orders.GetSingle(It.IsAny<Expression<Func<Order, bool>>>(),
                It.IsAny<bool>(),
                It.IsAny<string[]>()),
                Times.Once());
            
            mockUnitOfWork.Verify(x => x.Orders.Update(It.IsAny<Order>(),
                It.IsAny<Expression<Func<Order,bool>>>()), Times.Never);
            mockUnitOfWork.Verify(x => x.Commit(), Times.Never);
        }
    }
}