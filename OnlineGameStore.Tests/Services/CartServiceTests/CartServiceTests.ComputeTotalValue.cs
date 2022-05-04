using FluentAssertions;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.MVC.Services;
using OnlineGameStore.Tests.Helpers;
using System.Collections.Generic;
using Xunit;

namespace OnlineGameStore.Tests.Services
{
    public partial class CartServiceTests
    {
        [Theory]
        [AutoMoqData]
        public void CartService_CalculateCartTotal(
            Game game1,
            Game game2,
            CartService sut)
        {
            // Arrange
            sut.OrderDetails = new List<OrderDetail>();

            sut.AddItem(game1, 1);
            sut.AddItem(game2, 1);
            sut.AddItem(game1, 3);

            var expected = game1.Price * 4 + game2.Price;

            // Act
            var result = sut.ComputeTotalValue();

            // Assert
            result.Should().Be(expected);
        }
    }
}
