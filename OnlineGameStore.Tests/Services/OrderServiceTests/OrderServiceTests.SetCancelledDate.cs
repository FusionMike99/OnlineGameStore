using System;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
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
        public async Task OrderService_SetCancelledDate(
            DateTime cancelledDate,
            OrderModel order,
            [Frozen] Mock<IOrderRepository> orderRepositoryMock,
            OrderService sut)
        {
            // Arrange
            orderRepositoryMock.Setup(x => x.SetCancelledDateAsync(order.Id, cancelledDate));

            // Act
            await sut.SetCancelledDate(order.Id, cancelledDate);

            // Assert
            orderRepositoryMock.Verify(x => x.SetCancelledDateAsync(order.Id, cancelledDate), Times.Once);
        }
    }
}