﻿using System;
using System.Linq;
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
        public void OrderService_AddToOpenOrder_IncreaseQuantity_WhenGameIsAlreadyAdded(
            Game game,
            short quantity,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            OrderService sut)
        {
            // Arrange
            var order = GetTestOrder(game);

            mockUnitOfWork.Setup(x => x.Orders.GetSingle(It.IsAny<Expression<Func<Order, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<string[]>()))
                .Returns(order);

            mockUnitOfWork.Setup(x => x.Orders.Update(It.IsAny<Order>()));

            var arrangedOrderDetail = order.OrderDetails.First();

            var expectedQuantity = (short)(arrangedOrderDetail.Quantity + quantity);

            // Act
            sut.AddToOpenOrder(order.CustomerId, game, quantity);

            // Assert
            var resultOrderDetail = order.OrderDetails.First();
            resultOrderDetail.Quantity.Should().Be(expectedQuantity);

            mockUnitOfWork.Verify(x => x.Orders.GetSingle(It.IsAny<Expression<Func<Order, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<string[]>()),
                Times.Once);

            mockUnitOfWork.Verify(x => x.Orders.Update(It.IsAny<Order>()), Times.Once);
            mockUnitOfWork.Verify(x => x.Commit(), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public void OrderService_AddToOpenOrder_AddGame_WhenGameIsNotAlreadyAdded(
            Game game1,
            Game game2,
            short quantity,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            OrderService sut)
        {
            // Arrange
            var order = GetTestOrder(game1);

            mockUnitOfWork.Setup(x => x.Orders.GetSingle(It.IsAny<Expression<Func<Order, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<string[]>()))
                .Returns(order);

            mockUnitOfWork.Setup(x => x.Orders.Update(It.IsAny<Order>()));

            // Act
            sut.AddToOpenOrder(order.CustomerId, game2, quantity);

            // Assert
            order.OrderDetails.Should().HaveCount(2);

            mockUnitOfWork.Verify(x => x.Orders.GetSingle(It.IsAny<Expression<Func<Order, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<string[]>()),
                Times.Once);

            mockUnitOfWork.Verify(x => x.Orders.Update(It.IsAny<Order>()), Times.Once);
            mockUnitOfWork.Verify(x => x.Commit(), Times.Once);
        }
    }
}