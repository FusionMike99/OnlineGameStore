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
using OnlineGameStore.BLL.Repositories.GameStore;
using OnlineGameStore.BLL.Repositories.Northwind;

namespace OnlineGameStore.DAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IGameStoreOrderRepository _storeOrderRepository;
        private readonly INorthwindOrderRepository _northwindOrderRepository;
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;

        public OrderRepository(IGameStoreOrderRepository storeOrderRepository,
            INorthwindOrderRepository northwindOrderRepository,
            IGameRepository gameRepository,
            IMapper mapper)
        {
            _storeOrderRepository = storeOrderRepository;
            _northwindOrderRepository = northwindOrderRepository;
            _gameRepository = gameRepository;
            _mapper = mapper;
        }

        public async Task<OrderModel> GetOpenOrInProcessOrderAsync(Guid customerId)
        {
            var order = await _storeOrderRepository.GetOpenOrInProcessOrderAsync(customerId);
            var orderModel = _mapper.Map<OrderModel>(order);
            await SetOrderDetailsGames(orderModel.OrderDetails);

            return orderModel;
        }

        public async Task<IEnumerable<OrderModel>> GetOrdersAsync(FilterOrderModel filterOrderModel = null)
        {
            var sqlTask = _storeOrderRepository.GetOrdersAsync(filterOrderModel);
            var mongoTask = _northwindOrderRepository.GetOrdersAsync(filterOrderModel);
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
            await _storeOrderRepository.UpdateAsync(order);
        }

        public async Task AddProductToOrderAsync(Guid customerId, GameModel product, short quantity)
        {
            var game = _mapper.Map<GameEntity>(product);
            await _storeOrderRepository.AddProductToOrderAsync(customerId, game, quantity);
        }

        public async Task RemoveProductFromOrderAsync(Guid customerId, string gameKey)
        {
            await _storeOrderRepository.RemoveProductFromOrderAsync(customerId, gameKey);
        }

        public async Task<OrderModel> ChangeStatusToInProcessAsync(Guid customerId)
        {
            var order = await _storeOrderRepository.ChangeStatusToInProcessAsync(customerId);
            var orderModel = _mapper.Map<OrderModel>(order);
            await DecreaseGamesQuantities(orderModel.OrderDetails);

            return orderModel;
        }

        public async Task<OrderModel> ChangeStatusToClosedAsync(Guid orderId)
        {
            var order = await _storeOrderRepository.ChangeStatusToClosedAsync(orderId);
            var orderModel = _mapper.Map<OrderModel>(order);

            return orderModel;
        }

        public async Task<OrderModel> GetOrderByIdAsync(Guid orderId)
        {
            var order = await _storeOrderRepository.GetOrderByIdAsync(orderId);
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
            await _storeOrderRepository.SetCancelledDateAsync(orderId, cancelledDate);
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
            var orders = await _storeOrderRepository.GetOrdersWithStatusAsync(OrderState.InProgress);
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