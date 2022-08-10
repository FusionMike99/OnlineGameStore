using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Enums;
using OnlineGameStore.BLL.Models;
using OnlineGameStore.BLL.Repositories.GameStore;
using OnlineGameStore.DAL.Data;
using OnlineGameStore.DAL.Utils;

namespace OnlineGameStore.DAL.Repositories.GameStore
{
    public class GameStoreOrderRepository : GameStoreGenericRepository<Order>, IGameStoreOrderRepository
    {
        public GameStoreOrderRepository(StoreDbContext context) : base(context)
        {
        }

        public async Task<Order> GetOpenOrInProcessOrderAsync(Guid customerId)
        {
            Expression<Func<Order, bool>> predicate = o => o.CustomerId == customerId
                                                           && o.OrderState <= OrderState.InProgress
                                                           && o.CancelledDate == null;

            var order = await GetSingle(predicate, includeDeleted: false,
                includeProperties: $"{nameof(Order.OrderDetails)}");

            if (order != null)
            {
                return order;
            }
            
            var creatingOrder = new Order
            {
                CustomerId = customerId,
                OrderState = OrderState.Open,
                OrderDate = DateTime.UtcNow,
                OrderDetails = new List<OrderDetail>()
            };

            order = await Create(creatingOrder);

            return order;
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync(FilterOrderModel filterOrderModel = null)
        {
            var predicate = OrderPredicate.GetPredicate<Order>(filterOrderModel);

            var orders = await GetMany(predicate, includeDeleted: false,
                includeProperties: $"{nameof(Order.OrderDetails)}");

            return orders;
        }

        public async Task<IEnumerable<Order>> GetOrdersWithStatus(OrderState orderState)
        {
            Expression<Func<Order, bool>> predicate = o => o.OrderState == orderState;

            var orders = await GetMany(predicate, includeDeleted: false,
                includeProperties: $"{nameof(Order.OrderDetails)}");

            return orders;
        }

        public async Task AddProductToOrder(Guid customerId, Game product, short quantity)
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

            await Update(order);
        }

        public async Task RemoveProductFromOrder(Guid customerId, string gameKey)
        {
            var order = await GetOpenOrInProcessOrderAsync(customerId);

            var existOrderDetail = order.OrderDetails.SingleOrDefault(od => od.GameKey == gameKey);

            if (existOrderDetail == null)
            {
                return;
            }
            
            order.OrderDetails.Remove(existOrderDetail);

            await Update(order);
        }

        public async Task<Order> ChangeStatusToInProcess(Guid customerId)
        {
            var order = await GetOrderByCustomerId(customerId);
            CheckOrderExisting(order);

            order.OrderState = OrderState.InProgress;

            await Update(order);

            return order;
        }

        public async Task<Order> ChangeStatusToClosed(Guid orderId)
        {
            var order = await GetOrderById(orderId);
            CheckOrderExisting(order);
            
            order.OrderState = OrderState.Closed;

            await Update(order);

            return order;
        }

        public async Task<Order> GetOrderById(Guid orderId)
        {
            Expression<Func<Order, bool>> predicate = o => o.Id == orderId;

            var order = await GetSingle(predicate, includeDeleted: false,
                includeProperties: $"{nameof(Order.OrderDetails)}");

            return order;
        }

        public async Task SetCancelledDate(Guid orderId, DateTime cancelledDate)
        {
            var order = await GetOrderById(orderId);
            CheckOrderExisting(order);
            
            order.CancelledDate = cancelledDate;

            await Update(order);
        }
        
        private async Task<Order> GetOrderByCustomerId(Guid customerId, OrderState orderState = OrderState.Open)
        {
            Expression<Func<Order, bool>> predicate = o => o.CustomerId == customerId && o.OrderState == orderState;

            var order = await GetSingle(predicate, includeDeleted: false,
                includeProperties: $"{nameof(Order.OrderDetails)}");

            return order;
        }

        private static void CheckOrderExisting(Order order)
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