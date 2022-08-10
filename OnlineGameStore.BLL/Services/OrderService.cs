using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Models;
using OnlineGameStore.BLL.Models.General;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Services.Contracts;

namespace OnlineGameStore.BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderModel> GetOpenOrInProcessOrderAsync(Guid customerId)
        {
            var order = await _orderRepository.GetOpenOrInProcessOrderAsync(customerId);

            return order;
        }

        public async Task<IEnumerable<OrderModel>> GetOrders(FilterOrderModel filterOrderModel = null)
        {
            var orders = await _orderRepository.GetOrdersAsync(filterOrderModel);

            return orders;
        }

        public async Task<OrderModel> EditOrder(OrderModel order)
        {
            await _orderRepository.UpdateAsync(order);

            return order;
        }

        public async Task AddToOpenOrder(Guid customerId, GameModel product, short quantity)
        {
            await _orderRepository.AddProductToOrderAsync(customerId, product, quantity);
        }

        public async Task RemoveFromOrder(Guid customerId, string gameKey)
        {
            await _orderRepository.RemoveProductFromOrderAsync(customerId, gameKey);
        }

        public async Task<OrderModel> ChangeStatusToInProcess(Guid customerId)
        {
            var order = await _orderRepository.ChangeStatusToInProcessAsync(customerId);

            return order;
        }

        public async Task<OrderModel> ChangeStatusToClosed(Guid orderId)
        {
            var order = await _orderRepository.ChangeStatusToClosedAsync(orderId);

            return order;
        }

        public async Task<OrderModel> GetOrderById(Guid orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);

            return order;
        }

        public async Task CancelOrdersWithTimeout()
        {
            await _orderRepository.CancelOrdersWithTimeoutAsync();
        }

        public async Task SetCancelledDate(Guid orderId, DateTime cancelledDate)
        {
            await _orderRepository.SetCancelledDateAsync(orderId, cancelledDate);
        }
    }
}