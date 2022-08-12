using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Enums;
using OnlineGameStore.BLL.Models;
using OnlineGameStore.BLL.Repositories.GameStore;
using OnlineGameStore.DAL.Data;
using OnlineGameStore.DAL.Utils;

namespace OnlineGameStore.DAL.Repositories.GameStore
{
    public class GameStoreOrderRepository : GameStoreGenericRepository<OrderEntity>, IGameStoreOrderRepository
    {
        public GameStoreOrderRepository(StoreDbContext context, ILoggerFactory logger) : base(context, logger)
        {
        }

        public async Task<OrderEntity> GetOpenOrInProcessOrderAsync(Guid customerId)
        {
            Expression<Func<OrderEntity, bool>> predicate = o => o.CustomerId == customerId
                                                           && o.OrderState <= OrderState.InProgress
                                                           && o.CancelledDate == null;
            var order = await GetSingle(predicate, includeDeleted: false,
                includeProperties: $"{nameof(OrderEntity.OrderDetails)}");

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
            var predicate = OrderPredicate.GetPredicate<OrderEntity>(filterOrderModel);
            var orders = await GetMany(predicate, includeDeleted: false,
                includeProperties: $"{nameof(OrderEntity.OrderDetails)}");

            return orders;
        }

        public async Task<IEnumerable<OrderEntity>> GetOrdersWithStatusAsync(OrderState orderState)
        {
            Expression<Func<OrderEntity, bool>> predicate = o => o.OrderState == orderState;
            var orders = await GetMany(predicate, includeDeleted: false,
                includeProperties: $"{nameof(OrderEntity.OrderDetails)}");

            return orders;
        }

        public async Task AddProductToOrderAsync(Guid customerId, GameEntity product, short quantity)
        {
            var order = await GetOpenOrInProcessOrderAsync(customerId);
            var existOrderDetail = order.OrderDetails.SingleOrDefault(od => od.GameKey == product.Key);

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
            var existOrderDetail = order.OrderDetails.SingleOrDefault(od => od.GameKey == gameKey);

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
            Expression<Func<OrderEntity, bool>> predicate = o => o.Id == orderId;
            var order = await GetSingle(predicate, includeDeleted: false,
                includeProperties: $"{nameof(OrderEntity.OrderDetails)}");

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
            Expression<Func<OrderEntity, bool>> predicate = o => o.CustomerId == customerId && o.OrderState == orderState;
            var order = await GetSingle(predicate, includeDeleted: false,
                includeProperties: $"{nameof(OrderEntity.OrderDetails)}");

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