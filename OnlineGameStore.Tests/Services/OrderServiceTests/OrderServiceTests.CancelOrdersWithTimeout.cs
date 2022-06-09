using System;
using System.Linq;
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
            Game game,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            OrderService sut)
        {
            // Arrange
            var timeout = TimeSpan.FromMinutes(minutes);

            var orders = GetTestOrders(game, timeout);

            mockUnitOfWork.Setup(x => x.Orders.GetMany(It.IsAny<Expression<Func<Order, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<Func<IQueryable<Order>,IOrderedQueryable<Order>>>(),
                    It.IsAny<int?>(),
                    It.IsAny<int?>(),
                    It.IsAny<string[]>()))
                .Returns(orders);

            mockUnitOfWork.Setup(x => x.Games.GetSingle(It.IsAny<Expression<Func<Game, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<string[]>()))
                .Returns(game);

            mockUnitOfWork.Setup(x => x.Games.Update(It.IsAny<Game>()));

            // Act
            sut.CancelOrdersWithTimeout();

            // Assert
            mockUnitOfWork.Verify(x => x.Orders.GetMany(It.IsAny<Expression<Func<Order, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<Func<IQueryable<Order>,IOrderedQueryable<Order>>>(),
                    It.IsAny<int?>(),
                    It.IsAny<int?>(),
                    It.IsAny<string[]>()),
                Times.Once);

            mockUnitOfWork.Verify(x => x.Orders.Update(It.IsAny<Order>()), Times.Exactly(2));
            
            mockUnitOfWork.Verify(x => x.Games.GetSingle(It.IsAny<Expression<Func<Game, bool>>>(),
                It.IsAny<bool>(),
                It.IsAny<string[]>()),
                Times.Exactly(2));
            
            mockUnitOfWork.Verify(x => x.Games.Update(It.IsAny<Game>()), Times.Exactly(2));
            
            mockUnitOfWork.Verify(x => x.Commit(), Times.Once);
        }
    }
}