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

        private static IEnumerable<Order> GetTestOrders(Game game, TimeSpan timeSpan)
        {
            var timeoutTime = timeSpan.Add(TimeSpan.FromMinutes(1));
            
            var orderDetail = new OrderDetail
            {
                OrderId = 1,
                ProductId = game.Id,
                Product = game,
                Price = game.Price,
                Quantity = 4
            };

            var orders = new List<Order>
            {
                new Order
                {
                    Id = 1, CustomerId = 1,
                    OrderStatusId = 2, OrderDate = DateTime.UtcNow.Subtract(timeoutTime),
                    OrderDetails = new List<OrderDetail>{ orderDetail }
                },
                new Order
                {
                    Id = 2, CustomerId = 1,
                    OrderStatusId = 2, OrderDate = DateTime.UtcNow,
                    OrderDetails = new List<OrderDetail>{ orderDetail }
                },
                new Order
                {
                    Id = 3, CustomerId = 1,
                    OrderStatusId = 2, OrderDate = DateTime.UtcNow.Subtract(timeoutTime),
                    OrderDetails = new List<OrderDetail>{ orderDetail }
                },
                new Order
                {
                    Id = 4, CustomerId = 1,
                    OrderStatusId = 2, OrderDate = DateTime.UtcNow,
                    OrderDetails = new List<OrderDetail>{ orderDetail }
                }
            };

            return orders;
        }
    }
}