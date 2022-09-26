using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OnlineGameStore.DAL.Abstractions.Interfaces;
using OnlineGameStore.DAL.Entities;
using OnlineGameStore.DAL.Repositories.MongoDb.Interfaces;
using OnlineGameStore.DAL.Repositories.SqlServer.Interfaces;
using OnlineGameStore.DomainModels.Enums;
using OnlineGameStore.DomainModels.Models;
using OnlineGameStore.DomainModels.Models.General;

namespace OnlineGameStore.DAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IOrderSqlServerRepository _orderSqlServerRepository;
        private readonly IOrderMongoDbRepository _orderMongoDbRepository;
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;

        public OrderRepository(IOrderSqlServerRepository orderSqlServerRepository,
            IOrderMongoDbRepository orderMongoDbRepository,
            IGameRepository gameRepository,
            IMapper mapper)
        {
            _orderSqlServerRepository = orderSqlServerRepository;
            _orderMongoDbRepository = orderMongoDbRepository;
            _gameRepository = gameRepository;
            _mapper = mapper;
        }

        public async Task<OrderModel> GetOpenOrInProcessOrderAsync(Guid customerId)
        {
            var order = await _orderSqlServerRepository.GetOpenOrInProcessOrderAsync(customerId);
            var orderModel = _mapper.Map<OrderModel>(order);
            await SetOrderDetailsGames(orderModel.OrderDetails);

            return orderModel;
        }

        public async Task UpdateAsync(OrderModel orderModel)
        {
            var order = _mapper.Map<OrderEntity>(orderModel);
            await RefreshGamesQuantities(orderModel);
            RemoveZeroQuantityOrderDetails(order);
            await _orderSqlServerRepository.UpdateAsync(order);
        }

        public async Task AddProductToOrderAsync(Guid customerId, GameModel product, short quantity)
        {
            var game = _mapper.Map<GameEntity>(product);
            await _orderSqlServerRepository.AddProductToOrderAsync(customerId, game, quantity);
        }

        public async Task RemoveProductFromOrderAsync(Guid customerId, string gameKey)
        {
            await _orderSqlServerRepository.RemoveProductFromOrderAsync(customerId, gameKey);
        }

        public async Task<OrderModel> ChangeStatusToInProcessAsync(Guid customerId)
        {
            var order = await _orderSqlServerRepository.ChangeStatusToInProcessAsync(customerId);
            var orderModel = _mapper.Map<OrderModel>(order);
            await DecreaseGamesQuantities(orderModel.OrderDetails);

            return orderModel;
        }

        public async Task<OrderModel> ChangeStatusToClosedAsync(Guid orderId)
        {
            var order = await _orderSqlServerRepository.ChangeStatusToClosedAsync(orderId);
            var orderModel = _mapper.Map<OrderModel>(order);

            return orderModel;
        }

        public async Task<OrderModel> ChangeStatusToShippedAsync(Guid orderId)
        {
            var order = await _orderSqlServerRepository.ChangeStatusToShippedAsync(orderId);
            var orderModel = _mapper.Map<OrderModel>(order);

            return orderModel;
        }

        public async Task<OrderModel> GetOrderByIdAsync(Guid orderId)
        {
            var order = await _orderSqlServerRepository.GetOrderByIdAsync(orderId);
            var orderModel = _mapper.Map<OrderModel>(order);
            await SetOrderDetailsGames(orderModel.OrderDetails);

            return orderModel;
        }

        public async Task CancelOrdersWithTimeoutAsync()
        {
            var orders = await GetOrdersWithStatus(OrderState.InProgress);

            foreach (var order in orders)
            {
                if (order.CancelledDate == null || DateTime.UtcNow >= order.CancelledDate)
                {
                    continue;
                }

                await IncreaseGamesQuantities(order.OrderDetails);
                order.OrderState = OrderState.Cancelled;
                await UpdateAsync(order);
            }
        }

        public async Task SetCancelledDateAsync(Guid orderId, DateTime cancelledDate)
        {
            await _orderSqlServerRepository.SetCancelledDateAsync(orderId, cancelledDate);
        }

        public Task<IEnumerable<OrderModel>> GetOrdersAsync(FilterOrderModel filterOrderModel = null)
        {
            if (filterOrderModel == null || filterOrderModel.DatabaseEntity == DatabaseEntity.All)
            {
                return GetAllOrdersAsync();
            }

            if (filterOrderModel.DatabaseEntity == DatabaseEntity.GameStore)
            {
                return GetSqlServerOrdersAsync();
            }

            return GetMongoDbOrdersAsync();
        }

        private static void RemoveZeroQuantityOrderDetails(OrderEntity order)
        {
            foreach (var orderDetail in order.OrderDetails)
            {
                if (orderDetail.Quantity > 0)
                {
                    continue;
                }

                order.OrderDetails.Remove(orderDetail);
            }
        }

        private async Task RefreshGamesQuantities(OrderModel orderModel)
        {
            var oldOrderDetails = (await GetOrderByIdAsync(orderModel.Id)).OrderDetails;
            if (orderModel.OrderState >= OrderState.InProgress)
            {
                var decreaseOrderDetails = new List<OrderDetailModel>();
                var increaseOrderDetails = new List<OrderDetailModel>();

                foreach (var orderDetail in orderModel.OrderDetails)
                {
                    var oldOrderDetail = oldOrderDetails.FirstOrDefault(od => od.Id == orderDetail.Id);
                    if (oldOrderDetail == null)
                    {
                        continue;
                    }

                    var quantityDiff = (short)(orderDetail.Quantity - oldOrderDetail.Quantity);
                    if (quantityDiff > 0)
                    {
                        decreaseOrderDetails.Add(new OrderDetailModel
                        {
                            GameKey = orderDetail.GameKey,
                            Quantity = quantityDiff
                        });
                    }
                    else if (quantityDiff < 0)
                    {
                        increaseOrderDetails.Add(new OrderDetailModel
                        {
                            GameKey = orderDetail.GameKey,
                            Quantity = (short)(quantityDiff * -1)
                        });
                    }
                }

                await DecreaseGamesQuantities(decreaseOrderDetails);
                await IncreaseGamesQuantities(increaseOrderDetails);
            }
        }

        private async Task<IEnumerable<OrderModel>> GetAllOrdersAsync(FilterOrderModel filterOrderModel = null)
        {
            var sqlTask = _orderSqlServerRepository.GetOrdersAsync(filterOrderModel);
            var mongoTask = _orderMongoDbRepository.GetOrdersAsync(filterOrderModel);
            await Task.WhenAll(sqlTask, mongoTask);

            var orders = await sqlTask;
            var northwindOrder = await mongoTask;
            var mappedOrders = _mapper.Map<IEnumerable<OrderModel>>(orders);
            var mappedNorthwindOrders = _mapper.Map<IEnumerable<OrderModel>>(northwindOrder);
            var concatOrders = mappedOrders.Concat(mappedNorthwindOrders);

            return concatOrders;
        }

        private async Task<IEnumerable<OrderModel>> GetSqlServerOrdersAsync(FilterOrderModel filterOrderModel = null)
        {
            var orders = await _orderSqlServerRepository.GetOrdersAsync(filterOrderModel);
            var mappedOrders = _mapper.Map<IEnumerable<OrderModel>>(orders);

            return mappedOrders;
        }

        private async Task<IEnumerable<OrderModel>> GetMongoDbOrdersAsync(FilterOrderModel filterOrderModel = null)
        {
            var orders = await _orderMongoDbRepository.GetOrdersAsync(filterOrderModel);
            var mappedOrders = _mapper.Map<IEnumerable<OrderModel>>(orders);

            return mappedOrders;
        }

        private async Task IncreaseGamesQuantities(IEnumerable<OrderDetailModel> orderDetails)
        {
            foreach (var orderDetail in orderDetails)
            {
                await _gameRepository.IncreaseGameQuantityAsync(orderDetail.GameKey, orderDetail.Quantity);
            }
        }
        
        private async Task DecreaseGamesQuantities(IEnumerable<OrderDetailModel> orderDetails)
        {
            foreach (var orderDetail in orderDetails)
            {
                await _gameRepository.DecreaseGameQuantityAsync(orderDetail.GameKey, orderDetail.Quantity);
            }
        }

        private async Task<IEnumerable<OrderModel>> GetOrdersWithStatus(OrderState orderState)
        {
            var orders = await _orderSqlServerRepository.GetOrdersWithStatusAsync(orderState);
            var orderModels = _mapper.Map<IEnumerable<OrderModel>>(orders);

            return orderModels;
        }

        private async Task SetOrderDetailsGames(IEnumerable<OrderDetailModel> orderDetails)
        {
            if (orderDetails is null)
            {
                return;
            }

            foreach (var orderDetail in orderDetails)
            {
                orderDetail.Product = await _gameRepository.GetByKeyAsync(orderDetail.GameKey);
            }
        }
    }
}