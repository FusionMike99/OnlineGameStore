using System;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using OnlineGameStore.BLL.Services;
using OnlineGameStore.DAL.Abstractions.Interfaces;
using OnlineGameStore.DomainModels.Models.General;
using OnlineGameStore.Tests.Helpers;
using Xunit;

namespace OnlineGameStore.Tests.Services
{
    public partial class OrderServiceTests
    {
        [Theory]
        [AutoMoqData]
        public async Task OrderService_ChangeStatusToClosed_ReturnsOrder(
            OrderModel order,
            [Frozen] Mock<IOrderRepository> orderRepositoryMock,
            OrderService sut)
        {
            // Arrange
            orderRepositoryMock.Setup(x => x.ChangeStatusToClosedAsync(It.IsAny<Guid>()))
                .ReturnsAsync(order);

            // Act
            var actualOrder = await sut.ChangeStatusToClosedAsync(order.Id);

            // Assert
            actualOrder.OrderState.Should().Be(order.OrderState);

            orderRepositoryMock.Verify(x => x.ChangeStatusToClosedAsync(It.IsAny<Guid>()), Times.Once);
        }
    }
}