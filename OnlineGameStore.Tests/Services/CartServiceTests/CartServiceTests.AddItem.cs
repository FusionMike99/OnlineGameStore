using FluentAssertions;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.MVC.Services;
using OnlineGameStore.Tests.Helpers;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace OnlineGameStore.Tests.Services
{
    public partial class CartServiceTests
    {
        [Theory]
        [AutoMoqData]
        public void CartService_CanAddNewItem(
            Game game,
            CartService sut)
        {

            // Act
            sut.AddItem(game, 1);
            var results = sut.OrderDetails;

            // Assert
            results.Should().NotBeEmpty();
        }

        [Theory]
        [AutoMoqData]
        public void CartService_CanAddQuantityForExistingItem(
            Game game,
            CartService sut)
        {
            // Arrange
            sut.OrderDetails = new List<OrderDetail>();

            // Act
            sut.AddItem(game, 1);
            sut.AddItem(game, 10);
            var results = sut.OrderDetails.OrderBy(c => c.ProductId)
                .ToList();

            // Assert
            results.Should().NotBeEmpty()
                .And.BeInAscendingOrder(od => od.ProductId)
                .And.ContainSingle()
                    .Which.Quantity.Should().Be(11);
        }
    }
}
