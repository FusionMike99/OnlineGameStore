using System.Threading.Tasks;
using AutoFixture.Xunit2;
using Moq;
using OnlineGameStore.BLL.Services;
using OnlineGameStore.DAL.Abstractions.Interfaces;
using OnlineGameStore.Tests.Helpers;
using Xunit;

namespace OnlineGameStore.Tests.Services
{
    public partial class OrderServiceTests
    {
        [Theory]
        [AutoMoqData]
        public async Task OrderService_CancelOrderWithTimeout(
            [Frozen] Mock<IOrderRepository> orderRepositoryMock,
            OrderService sut)
        {
            // Arrange
            orderRepositoryMock.Setup(x => x.CancelOrdersWithTimeoutAsync());

            // Act
            await sut.CancelOrdersWithTimeoutAsync();

            // Assert
            orderRepositoryMock.Verify(x => x.CancelOrdersWithTimeoutAsync(), Times.Once);
        }
    }
}