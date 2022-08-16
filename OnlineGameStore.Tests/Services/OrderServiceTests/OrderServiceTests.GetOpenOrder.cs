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
        public async Task OrderService_GetOpenOrder_ReturnsOrder_WhenExistsOpenOrder(
            OrderModel order,
            [Frozen] Mock<IOrderRepository> orderRepositoryMock,
            OrderService sut)
        {
            // Arrange
            orderRepositoryMock.Setup(x => x.GetOpenOrInProcessOrderAsync(It.IsAny<Guid>()))
                .ReturnsAsync(order);

            // Act
            var actualOrder = await sut.GetOpenOrInProcessOrderAsync(order.CustomerId);

            // Assert
            actualOrder.Should().BeEquivalentTo(order);

            orderRepositoryMock.Verify(x => x.GetOpenOrInProcessOrderAsync(It.IsAny<Guid>()), Times.Once);
        }
    }
}