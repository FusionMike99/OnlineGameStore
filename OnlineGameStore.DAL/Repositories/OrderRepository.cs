using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Enums;
using OnlineGameStore.BLL.Models;
using OnlineGameStore.BLL.Models.General;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Repositories.MongoDb;
using OnlineGameStore.BLL.Repositories.SqlServer;

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

        public async Task<IEnumerable<OrderModel>> GetOrdersAsync(FilterOrderModel filterOrderModel = null)
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

        public async Task UpdateAsync(OrderModel orderModel)
        {
            var order = _mapper.Map<OrderEntity>(orderModel);
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

        public async Task<OrderModel> GetOrderByIdAsync(Guid orderId)
        {
            var order = await _orderSqlServerRepository.GetOrderByIdAsync(orderId);
            var orderModel = _mapper.Map<OrderModel>(order);

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
            var orders = await _orderSqlServerRepository.GetOrdersWithStatusAsync(OrderState.InProgress);
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