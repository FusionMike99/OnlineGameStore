using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using OnlineGameStore.BLL.Models;
using OnlineGameStore.BLL.Models.General;
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
        public async Task OrderService_GetOrders_ReturnsOrders(
            List<OrderModel> orders,
            FilterOrderModel filterOrderModel,
            [Frozen] Mock<IOrderRepository> orderRepositoryMock,
            OrderService sut)
        {
            // Arrange
            orderRepositoryMock.Setup(x => x.GetOrdersAsync(It.IsAny<FilterOrderModel>()))
                .ReturnsAsync(orders);

            // Act
            var actualOrder = await sut.GetOrders(filterOrderModel);

            // Assert
            actualOrder.Should().BeEquivalentTo(orders);
            orderRepositoryMock.Verify(x => x.GetOrdersAsync(It.IsAny<FilterOrderModel>()), Times.Once);
        }
    }
}