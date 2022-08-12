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
        public async Task OrderService_AddToOpenOrder_IncreaseQuantity_WhenGameIsAlreadyAdded(
            OrderModel order,
            GameModel game,
            short quantity,
            [Frozen] Mock<IOrderRepository> orderRepositoryMock,
            OrderService sut)
        {
            // Arrange
            orderRepositoryMock.Setup(x => x.AddProductToOrderAsync(order.CustomerId, game, quantity));

            // Act
            await sut.AddToOpenOrderAsync(order.CustomerId, game, quantity);

            // Assert
            orderRepositoryMock.Verify(x => x.AddProductToOrderAsync(order.CustomerId, game, quantity),
                Times.Once);
        }
    }
}