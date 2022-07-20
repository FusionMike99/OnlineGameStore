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
                GameKey = game.Key,
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
                CustomerId = "6A5CFAE0-C015-4AAA-9E7E-4C344BB0D287",
                OrderStatusId = 1
            };

            return order;
        }

        private static IEnumerable<Order> GetTestOrders(Game game, TimeSpan timeSpan)
        {
            var timeoutTime = timeSpan.Add(TimeSpan.FromMinutes(1));
            
            var orderDetail = new OrderDetail
            {
                OrderId = 1,
                GameKey = game.Key,
                Product = game,
                Price = game.Price,
                Quantity = 4
            };

            var orders = new List<Order>
            {
                new Order
                {
                    Id = 1, CustomerId = "B29B58C9-3A63-4EF5-A71F-96A71C9A26BD",
                    OrderStatusId = 2, CancelledDate = DateTime.UtcNow.Subtract(timeoutTime),
                    OrderDetails = new List<OrderDetail>{ orderDetail }
                },
                new Order
                {
                    Id = 2, CustomerId = "B29B58C9-3A63-4EF5-A71F-96A71C9A26BD",
                    OrderStatusId = 2, CancelledDate = DateTime.UtcNow.Add(timeoutTime),
                    OrderDetails = new List<OrderDetail>{ orderDetail }
                },
                new Order
                {
                    Id = 3, CustomerId = "B29B58C9-3A63-4EF5-A71F-96A71C9A26BD",
                    OrderStatusId = 2, CancelledDate = DateTime.UtcNow.Subtract(timeoutTime),
                    OrderDetails = new List<OrderDetail>{ orderDetail }
                },
                new Order
                {
                    Id = 4, CustomerId = "B29B58C9-3A63-4EF5-A71F-96A71C9A26BD",
                    OrderStatusId = 2, CancelledDate = DateTime.UtcNow.Add(timeoutTime),
                    OrderDetails = new List<OrderDetail>{ orderDetail }
                }
            };

            return orders;
        }
    }
}