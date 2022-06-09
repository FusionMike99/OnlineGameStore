using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Enums;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Services.Contracts;

namespace OnlineGameStore.BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly ILogger<OrderService> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork,
            ILogger<OrderService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public Order GetOpenOrder(int customerId)
        {
            var order = _unitOfWork.Orders
                .GetSingle(o => o.CustomerId == customerId 
                                && o.OrderStatusId <= (int)OrderState.InProgress && o.CancelledDate == null,
                    false,
                    $"{nameof(Order.OrderDetails)}.{nameof(OrderDetail.Product)}");

            if (order == null)
            {
                var creatingOrder = new Order
                {
                    CustomerId = customerId,
                    OrderStatusId = (int)OrderState.Open,
                    OrderDate = DateTime.UtcNow,
                    OrderDetails = new List<OrderDetail>()
                };

                order = _unitOfWork.Orders.Create(creatingOrder);
                _unitOfWork.Commit();
            }

            _logger.LogDebug($@"Class: {nameof(OrderService)}; Method: {nameof(GetOpenOrder)}.
                    Opening order for customer with id {customerId} successfully", order);

            return order;
        }

        public void AddToOpenOrder(int customerId, Game product, short quantity)
        {
            var order = GetOpenOrder(customerId);

            var existOrderDetail = order.OrderDetails.SingleOrDefault(od => od.ProductId == product.Id);

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
                    ProductId = product.Id,
                    Quantity = quantity <= product.UnitsInStock ? quantity : product.UnitsInStock,
                    Price = product.Price
                };

                order.OrderDetails.Add(orderDetail);
            }

            _unitOfWork.Orders.Update(order);
            _unitOfWork.Commit();

            _logger.LogDebug($@"Class: {nameof(OrderService)}; Method: {nameof(AddToOpenOrder)}.
                    Adding order detail to order successfully", order);
        }

        public void RemoveFromOrder(int customerId, int productId)
        {
            var order = GetOpenOrder(customerId);

            var existOrderDetail = order.OrderDetails.SingleOrDefault(od => od.ProductId == productId);

            if (existOrderDetail == null)
            {
                return;
            }
            
            order.OrderDetails.Remove(existOrderDetail);

            _unitOfWork.Orders.Update(order);
            _unitOfWork.Commit();
            
            _logger.LogDebug($@"Class: {nameof(OrderService)}; Method: {nameof(RemoveFromOrder)}.
                    Removing from order successfully", order);
        }

        public Order ChangeStatusToInProcess(int customerId)
        {
            var order = GetOrderByCustomerId(customerId);

            CheckOrderExisting(order);

            order.OrderStatusId = (int)OrderState.InProgress;

            UpdateGamesQuantities(order.OrderDetails, (a, b) => (short)(a - b));

            _unitOfWork.Orders.Update(order);
            _unitOfWork.Commit();

            _logger.LogDebug($@"Class: {nameof(OrderService)}; Method: {nameof(ChangeStatusToInProcess)}.
                    Changing order with id {order.Id} 'in processing' successfully", order);

            return order;
        }

        public Order ChangeStatusToClosed(int orderId)
        {
            var order = GetOrderById(orderId);

            CheckOrderExisting(order);
            
            order.OrderStatusId = (int)OrderState.Closed;

            _unitOfWork.Orders.Update(order);
            _unitOfWork.Commit();

            _logger.LogDebug($@"Class: {nameof(OrderService)}; Method: {nameof(ChangeStatusToClosed)}.
                    Changing order with id {order.Id} 'closed' successfully", order);

            return order;
        }

        public Order GetOrderById(int orderId)
        {
            var order = _unitOfWork.Orders.GetSingle(o => o.Id == orderId,
                    false,
                    $"{nameof(Order.OrderDetails)}.{nameof(OrderDetail.Product)}");

            _logger.LogDebug($@"Class: {nameof(OrderService)}; Method: {nameof(GetOrderById)}.
                    Receiving order with id {orderId} successfully", order);

            return order;
        }

        public void CancelOrdersWithTimeout()
        {
            var orders = GetOrdersWithStatus(OrderState.InProgress);

            foreach (var order in orders)
            {
                if (order.CancelledDate == null || DateTime.UtcNow >= order.CancelledDate)
                {
                    continue;
                }

                UpdateGamesQuantities(order.OrderDetails, (a, b) => (short)(a + b));
                
                order.OrderStatusId = (int)OrderState.Cancelled;

                _unitOfWork.Orders.Update(order);
            }
            
            _unitOfWork.Commit();
            
            _logger.LogDebug($@"Class: {nameof(OrderService)}; Method: {nameof(CancelOrdersWithTimeout)}.
                    Cancelling orders successfully", orders);
        }

        public void SetCancelledDate(int orderId, DateTime cancelledDate)
        {
            var order = GetOrderById(orderId);
            
            CheckOrderExisting(order);
            
            order.CancelledDate = cancelledDate;

            _unitOfWork.Orders.Update(order);
            _unitOfWork.Commit();

            _logger.LogDebug($@"Class: {nameof(OrderService)}; Method: {nameof(SetCancelledDate)}.
                    Setting cancelled date for order with id {order.Id} 'closed' successfully", order);
        }

        private Order GetOrderByCustomerId(int customerId, OrderState orderState = OrderState.Open)
        {
            var order = _unitOfWork.Orders
                .GetSingle(o => o.CustomerId == customerId && o.OrderStatusId == (int)orderState,
                    false,
                    $"{nameof(Order.OrderDetails)}.{nameof(OrderDetail.Product)}");

            _logger.LogDebug($@"Class: {nameof(OrderService)}; Method: {nameof(GetOrderByCustomerId)}.
                    Receiving order with status id {(int)orderState} successfully", order);

            return order;
        }

        private IEnumerable<Order> GetOrdersWithStatus(OrderState orderState)
        {
            var orders = _unitOfWork.Orders.GetMany(o => o.OrderStatusId == (int)orderState,
                    false,
                    null,
                    null,
                    null,
                    $"{nameof(Order.OrderDetails)}.{nameof(OrderDetail.Product)}");

            _logger.LogDebug($@"Class: {nameof(OrderService)}; Method: {nameof(GetOrdersWithStatus)}.
                    Receiving orders with status id {(int)orderState} successfully", orders);

            return orders;
        }

        private void UpdateGamesQuantities(IEnumerable<OrderDetail> orderDetails,
            Func<short, short, short> operation)
        {
            foreach (var orderDetail in orderDetails)
            {
                var game = _unitOfWork.Games.GetSingle(g => g.Id == orderDetail.ProductId);

                game.UnitsInStock = operation(game.UnitsInStock, orderDetail.Quantity);

                _unitOfWork.Games.Update(game);
            }
        }

        private void CheckOrderExisting(Order order)
        {
            if (order != null)
            {
                return;
            }

            var exception = new InvalidOperationException("Order has not been found");

            _logger.LogError(exception,
                $@"Class: {nameof(OrderService)}; Method: {nameof(CheckOrderExisting)}.
                    Changing order status unsuccessfully");

            throw exception;
        }
    }
}