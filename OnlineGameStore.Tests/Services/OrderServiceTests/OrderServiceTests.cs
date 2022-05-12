using System;
using System.Collections.Generic;
using OnlineGameStore.BLL.Entities;

namespace OnlineGameStore.Tests.Services
{
    public partial class OrderServiceTests
    {
        private static Order GetTestOrder(Game game)
        {
            var orderDetail = new OrderDetail
            {
                OrderId = 1,
                ProductId = game.Id,
                Product = game,
                Price = game.Price,
                Quantity = 4
            };

            var order = new Order
            {
                Id = 1,
                OrderDetails = new List<OrderDetail>
                {
                    orderDetail
                },
                CustomerId = 1,
                OrderStatusId = 1
            };

            return order;
        }

        private static IEnumerable<Order> GetTestOrders(TimeSpan timeSpan)
        {
            var timeoutTime = timeSpan.Add(TimeSpan.FromMinutes(1));

            var orders = new List<Order>
            {
                new Order
                {
                    Id = 1, CustomerId = 1,
                    OrderStatusId = 2, OrderDate = DateTime.UtcNow.Subtract(timeoutTime)
                },
                new Order
                {
                    Id = 2, CustomerId = 1,
                    OrderStatusId = 2, OrderDate = DateTime.UtcNow
                },
                new Order
                {
                    Id = 3, CustomerId = 1,
                    OrderStatusId = 2, OrderDate = DateTime.UtcNow.Subtract(timeoutTime)
                },
                new Order
                {
                    Id = 4, CustomerId = 1,
                    OrderStatusId = 2, OrderDate = DateTime.UtcNow
                }
            };

            return orders;
        }
    }
}