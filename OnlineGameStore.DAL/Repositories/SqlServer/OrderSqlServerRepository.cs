using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineGameStore.DAL.Data;
using OnlineGameStore.DAL.Entities;
using OnlineGameStore.DAL.Repositories.SqlServer.Extensions;
using OnlineGameStore.DAL.Repositories.SqlServer.Interfaces;
using OnlineGameStore.DAL.Utils;
using OnlineGameStore.DomainModels.Enums;
using OnlineGameStore.DomainModels.Models;

namespace OnlineGameStore.DAL.Repositories.SqlServer
{
    public class OrderSqlServerRepository : SqlServerRepository<OrderEntity>, IOrderSqlServerRepository
    {
        public OrderSqlServerRepository(StoreDbContext context, ILoggerFactory logger) : base(context, logger)
        {
        }

        public async Task<OrderEntity> GetOpenOrInProcessOrderAsync(Guid customerId)
        {
            var order = await Entities.IncludeForOrders().FirstOrDefaultAsync(o => o.CustomerId == customerId
                                                                && o.OrderState <= OrderState.InProgress
                                                                && o.CancelledDate == null);
            if (order != null)
            {
                return order;
            }
            
            var creatingOrder = new OrderEntity
            {
                CustomerId = customerId,
                OrderState = OrderState.Open,
                OrderDate = DateTime.UtcNow,
                OrderDetails = new List<OrderDetail>()
            };
            order = await CreateAsync(creatingOrder);

            return order;
        }

        public async Task<IEnumerable<OrderEntity>> GetOrdersAsync(FilterOrderModel filterOrderModel = null)
        {
            var orders = Entities.IncludeForOrders();
            
            var predicate = OrderPredicate.GetPredicate<OrderEntity>(filterOrderModel);
            if (predicate != null)
            {
                orders = orders.Where(predicate);
            }
            
            var ordersList = await orders.ToListAsync();

            return ordersList;
        }

        public async Task<IEnumerable<OrderEntity>> GetOrdersWithStatusAsync(OrderState orderState)
        {
            var orders = await Entities.IncludeForOrders()
                .Where(o => o.OrderState == orderState).ToListAsync();

            return orders;
        }

        public async Task AddProductToOrderAsync(Guid customerId, GameEntity product, short quantity)
        {
            var order = await GetOpenOrInProcessOrderAsync(customerId);
            var existOrderDetail = order.OrderDetails.FirstOrDefault(od => od.GameKey == product.Key);

            if (existOrderDetail != null)
            {
                var newQuantity = (short)(existOrderDetail.Quantity + quantity);
                existOrderDetail.Quantity = product.UnitsInStock - newQuantity >= 0 
                    ? newQuantity : product.UnitsInStock;
            }
            else
            {
                var orderDetail = new OrderDetail
                {
                    OrderId = order.Id,
                    GameKey = product.Key,
                    Quantity = quantity <= product.UnitsInStock ? quantity : product.UnitsInStock,
                    Price = product.Price
                };
                order.OrderDetails.Add(orderDetail);
            }

            await UpdateAsync(order);
        }

        public async Task RemoveProductFromOrderAsync(Guid customerId, string gameKey)
        {
            var order = await GetOpenOrInProcessOrderAsync(customerId);
            var existOrderDetail = order.OrderDetails.FirstOrDefault(od => od.GameKey == gameKey);

            if (existOrderDetail == null)
            {
                return;
            }
            
            order.OrderDetails.Remove(existOrderDetail);
            await UpdateAsync(order);
        }

        public async Task<OrderEntity> ChangeStatusToInProcessAsync(Guid customerId)
        {
            var order = await GetOrderByCustomerId(customerId);
            CheckOrderExisting(order);
            order.OrderState = OrderState.InProgress;
            await UpdateAsync(order);

            return order;
        }

        public async Task<OrderEntity> ChangeStatusToClosedAsync(Guid orderId)
        {
            var order = await GetOrderByIdAsync(orderId);
            CheckOrderExisting(order);
            order.OrderState = OrderState.Closed;
            await UpdateAsync(order);

            return order;
        }

        public async Task<OrderEntity> GetOrderByIdAsync(Guid orderId)
        {
            var order = await Entities.IncludeForOrders().FirstOrDefaultAsync(o => o.Id == orderId);

            return order;
        }

        public async Task SetCancelledDateAsync(Guid orderId, DateTime cancelledDate)
        {
            var order = await GetOrderByIdAsync(orderId);
            CheckOrderExisting(order);
            order.CancelledDate = cancelledDate;
            await UpdateAsync(order);
        }
        
        private async Task<OrderEntity> GetOrderByCustomerId(Guid customerId, OrderState orderState = OrderState.Open)
        {
            var order = await Entities.IncludeForOrders()
                .FirstOrDefaultAsync(o => o.CustomerId == customerId && o.OrderState == orderState);

            return order;
        }

        private static void CheckOrderExisting(OrderEntity order)
        {
            if (order != null)
            {
                return;
            }

            var exception = new InvalidOperationException("Order has not been found");
            throw exception;
        }
    }
}