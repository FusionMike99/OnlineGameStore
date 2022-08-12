using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
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
        public async Task OrderService_EditOrder_ReturnsOrder(
            OrderModel order,
            [Frozen] Mock<IOrderRepository> orderRepositoryMock,
            OrderService sut)
        {
            // Arrange
            orderRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<OrderModel>()));

            // Act
            var actualOrder = await sut.EditOrderAsync(order);

            // Assert
            actualOrder.Should().BeEquivalentTo(order);

            orderRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<OrderModel>()), Times.Once);
        }
    }
}