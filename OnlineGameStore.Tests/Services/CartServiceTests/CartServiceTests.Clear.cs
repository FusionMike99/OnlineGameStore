using FluentAssertions;
using OnlineGameStore.MVC.Services;
using OnlineGameStore.Tests.Helpers;
using Xunit;

namespace OnlineGameStore.Tests.Services
{
    public partial class CartServiceTests
    {
        [Theory]
        [AutoMoqData]
        public void CartService_CanClearContents(
            CartService sut)
        {
            // Act
            sut.Clear();
            var results = sut.OrderDetails;

            // Assert
            results.Should().BeEmpty();
        }
    }
}
