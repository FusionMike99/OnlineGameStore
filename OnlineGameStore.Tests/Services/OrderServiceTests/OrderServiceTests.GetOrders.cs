using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using OnlineGameStore.BLL.Services;
using OnlineGameStore.DAL.Abstractions.Interfaces;
using OnlineGameStore.DomainModels.Models;
using OnlineGameStore.DomainModels.Models.General;
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
            var actualOrder = await sut.GetOrdersAsync(filterOrderModel);

            // Assert
            actualOrder.Should().BeEquivalentTo(orders);
            orderRepositoryMock.Verify(x => x.GetOrdersAsync(It.IsAny<FilterOrderModel>()), Times.Once);
        }
    }
}