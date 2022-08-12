using System;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using Moq;
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
        public async Task OrderService_RemoveFromOrder_RemoveOrderDetail_WhenPositionIsExist(
            Guid customerId,
            string gameKey,
            [Frozen] Mock<IOrderRepository> orderRepositoryMock,
            OrderService sut)
        {
            // Arrange
            orderRepositoryMock.Setup(x => x.RemoveProductFromOrderAsync(customerId, gameKey));

            // Act
            await sut.RemoveFromOrderAsync(customerId, gameKey);

            // Assert
            orderRepositoryMock.Verify(x => x.RemoveProductFromOrderAsync(customerId, gameKey), Times.Once());
        }
    }
}