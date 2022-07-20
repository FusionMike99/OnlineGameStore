using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.Extensions.Logging;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Entities.Northwind;
using OnlineGameStore.BLL.Enums;
using OnlineGameStore.BLL.Models;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Services.Contracts;
using OnlineGameStore.BLL.Utils;

namespace OnlineGameStore.BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly ILogger<OrderService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly INorthwindUnitOfWork _northwindUnitOfWork;
        private readonly IGameService _gameService;
        private readonly INorthwindLogService _logService;
        private readonly IMapper _mapper;

        public OrderService(ILogger<OrderService> logger,
            IUnitOfWork unitOfWork,
            INorthwindUnitOfWork northwindUnitOfWork,
            IGameService gameService,
            INorthwindLogService logService,
            IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _northwindUnitOfWork = northwindUnitOfWork;
            _gameService = gameService;
            _logService = logService;
            _mapper = mapper;
        }

        public Order GetOpenOrder(string customerId)
        {
            var order = _unitOfWork.Orders
                .GetSingle(o => o.CustomerId == customerId 
                                && o.OrderStatusId <= (int)OrderState.InProgress && o.CancelledDate == null,
                    false, $"{nameof(Order.OrderDetails)}");

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
                
                _logService.LogCreating(creatingOrder);
            }
            
            SetOrderDetailsGames(order.OrderDetails);

            _logger.LogDebug($@"Class: {nameof(OrderService)}; Method: {nameof(GetOpenOrder)}.
                    Opening order for customer with id {customerId} successfully", order);

            return order;
        }

        public IEnumerable<Order> GetOrders(FilterOrderModel filterOrderModel = null)
        {
            var (gameStorePredicate, northwindPredicate) = GetPredicates(filterOrderModel);

            var orders = _unitOfWork.Orders.GetMany(gameStorePredicate,
                false, null, null, null, $"{nameof(Order.OrderDetails)}");
            var northwindOrder = _northwindUnitOfWork.Orders.GetMany(northwindPredicate).ToList();
            northwindOrder.ForEach(o =>
            {
                o.OrderDetails = _northwindUnitOfWork.OrderDetails
                    .GetMany(od => od.OrderId == o.OrderId);
                o.Shipper = _northwindUnitOfWork.Shippers.GetFirst(s => s.ShipperId == o.ShipVia);
            });

            var mappedNorthwindOrders = _mapper.Map<IEnumerable<Order>>(northwindOrder);
            var concatOrders = orders.Concat(mappedNorthwindOrders);

            return concatOrders;
        }

        public Order EditOrder(Order order)
        {
            var oldOrder = GetOrderById(order.Id);
            
            var editedOrder = _unitOfWork.Orders.Update(order);
            _unitOfWork.Commit();

            _logger.LogDebug($@"Class: {nameof(OrderService)}; Method: {nameof(EditOrder)}.
                    Editing order with id {editedOrder.Id} successfully", editedOrder);
            
            _logService.LogUpdating(oldOrder, editedOrder);

            return editedOrder;
        }

        public void AddToOpenOrder(string customerId, Game product, short quantity)
        {
            var order = GetOpenOrder(customerId);
            var oldOrder = order.DeepClone();

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

            _unitOfWork.Orders.Update(order);
            _unitOfWork.Commit();

            _logger.LogDebug($@"Class: {nameof(OrderService)}; Method: {nameof(AddToOpenOrder)}.
                    Adding order detail to order successfully", order);
            
            _logService.LogUpdating(oldOrder, order);
        }

        public void RemoveFromOrder(string customerId, string gameKey)
        {
            var order = GetOpenOrder(customerId);
            var oldOrder = order.DeepClone();

            var existOrderDetail = order.OrderDetails.SingleOrDefault(od => od.GameKey == gameKey);

            if (existOrderDetail == null)
            {
                return;
            }
            
            order.OrderDetails.Remove(existOrderDetail);

            _unitOfWork.Orders.Update(order);
            _unitOfWork.Commit();
            
            _logger.LogDebug($@"Class: {nameof(OrderService)}; Method: {nameof(RemoveFromOrder)}.
                    Removing from order successfully", order);
            
            _logService.LogUpdating(oldOrder, order);
        }

        public Order ChangeStatusToInProcess(string customerId)
        {
            var order = GetOrderByCustomerId(customerId);
            CheckOrderExisting(order);
            var oldOrder = order.DeepClone();

            order.OrderStatusId = (int)OrderState.InProgress;

            UpdateGamesQuantities(order.OrderDetails, (a, b) => (short)(a - b));

            _unitOfWork.Orders.Update(order);
            _unitOfWork.Commit();

            _logger.LogDebug($@"Class: {nameof(OrderService)}; Method: {nameof(ChangeStatusToInProcess)}.
                    Changing order with id {order.Id} 'in processing' successfully", order);
            
            _logService.LogUpdating(oldOrder, order);

            return order;
        }

        public Order ChangeStatusToClosed(int orderId)
        {
            var order = GetOrderById(orderId);
            CheckOrderExisting(order);
            var oldOrder = order.DeepClone();
            
            order.OrderStatusId = (int)OrderState.Closed;

            _unitOfWork.Orders.Update(order);
            _unitOfWork.Commit();

            _logger.LogDebug($@"Class: {nameof(OrderService)}; Method: {nameof(ChangeStatusToClosed)}.
                    Changing order with id {order.Id} 'closed' successfully", order);
            
            _logService.LogUpdating(oldOrder, order);

            return order;
        }

        public Order GetOrderById(int orderId)
        {
            var order = _unitOfWork.Orders.GetSingle(o => o.Id == orderId,
                    false, $"{nameof(Order.OrderDetails)}");

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

                var oldOrder = order.DeepClone();

                UpdateGamesQuantities(order.OrderDetails, (a, b) => (short)(a + b));
                
                order.OrderStatusId = (int)OrderState.Cancelled;

                _unitOfWork.Orders.Update(order);
                
                _logService.LogUpdating(oldOrder, order);
            }
            
            _unitOfWork.Commit();
            
            _logger.LogDebug($@"Class: {nameof(OrderService)}; Method: {nameof(CancelOrdersWithTimeout)}.
                    Cancelling orders successfully", orders);
        }

        public void SetCancelledDate(int orderId, DateTime cancelledDate)
        {
            var order = GetOrderById(orderId);
            CheckOrderExisting(order);
            var oldOrder = order.DeepClone();
            
            order.CancelledDate = cancelledDate;

            _unitOfWork.Orders.Update(order);
            _unitOfWork.Commit();

            _logger.LogDebug($@"Class: {nameof(OrderService)}; Method: {nameof(SetCancelledDate)}.
                    Setting cancelled date for order with id {order.Id} 'closed' successfully", order);
            
            _logService.LogUpdating(oldOrder, order);
        }

        private Order GetOrderByCustomerId(string customerId, OrderState orderState = OrderState.Open)
        {
            var order = _unitOfWork.Orders
                .GetSingle(o => o.CustomerId == customerId && o.OrderStatusId == (int)orderState,
                    false, $"{nameof(Order.OrderDetails)}");

            SetOrderDetailsGames(order?.OrderDetails);

            _logger.LogDebug($@"Class: {nameof(OrderService)}; Method: {nameof(GetOrderByCustomerId)}.
                    Receiving order with status id {(int)orderState} successfully", order);

            return order;
        }

        private IEnumerable<Order> GetOrdersWithStatus(OrderState orderState)
        {
            var orders = _unitOfWork.Orders.GetMany(o => o.OrderStatusId == (int)orderState,
                    false, null, null, null, $"{nameof(Order.OrderDetails)}");

            _logger.LogDebug($@"Class: {nameof(OrderService)}; Method: {nameof(GetOrdersWithStatus)}.
                    Receiving orders with status id {(int)orderState} successfully", orders);

            return orders;
        }

        private void UpdateGamesQuantities(IEnumerable<OrderDetail> orderDetails,
            Func<short, short, short> operation)
        {
            foreach (var orderDetail in orderDetails)
            {
                _gameService.UpdateGameQuantity(orderDetail.GameKey, orderDetail.Quantity, operation);
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

        private static (Expression<Func<Order, bool>>, Expression<Func<NorthwindOrder, bool>>)
            GetPredicates(FilterOrderModel model)
        {
            return (GetPredicate<Order>(model), GetPredicate<NorthwindOrder>(model));
        }

        private static Expression<Func<TEntity, bool>> GetPredicate<TEntity>(FilterOrderModel model)
        {
            if (model == null)
            {
                return null;
            }
            
            Expression<Func<TEntity, bool>> filterExpression = null;
            
            var orderParameter = Expression.Parameter(typeof(TEntity));
            var orderDateProperty = Expression.Property(orderParameter,
                typeof(TEntity).GetProperty("OrderDate")!);
            
            if (model.MinDate.HasValue)
            {
                model.MinDate = DateTime.SpecifyKind(model.MinDate.Value, DateTimeKind.Utc);
                
                var minDateConstant = Expression.Constant(model.MinDate.Value.Date, model.MinDate.GetType());
                var comparison = Expression.GreaterThanOrEqual(orderDateProperty, minDateConstant);
                filterExpression = Expression.Lambda<Func<TEntity, bool>>(comparison, orderParameter);
            }
            
            if (model.MaxDate.HasValue)
            {
                model.MaxDate = DateTime.SpecifyKind(model.MaxDate.Value, DateTimeKind.Utc);
                
                var maxDateConstant = Expression.Constant(model.MaxDate.Value.Date, model.MaxDate.GetType());
                var comparison = Expression.LessThanOrEqual(orderDateProperty, maxDateConstant);
                var otherExpression = Expression.Lambda<Func<TEntity, bool>>(comparison, orderParameter);
                
                filterExpression = filterExpression != null 
                    ? filterExpression.AndAlso(otherExpression)
                    : otherExpression;
            }

            return filterExpression;
        }

        private void SetOrderDetailsGames(IEnumerable<OrderDetail> orderDetails)
        {
            if (orderDetails is null)
            {
                return;
            }

            foreach (var orderDetail in orderDetails)
            {
                orderDetail.Product = _gameService.GetGameByKey(orderDetail.GameKey);
            }
        }
    }
}