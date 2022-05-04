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
        public void CartService_CanRemoveLine(
            Game game,
            CartService sut)
        {
            // Arrange
            sut.OrderDetails = new List<OrderDetail>();
            sut.AddItem(game, 1);

            // Act
            sut.RemoveItem(game);
            var results = sut.OrderDetails;

            // Assert
            results.Should().BeEmpty();
        }
    }
}
