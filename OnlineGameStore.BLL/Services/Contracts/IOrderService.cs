using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Models;
using OnlineGameStore.BLL.Models.General;

namespace OnlineGameStore.BLL.Services.Contracts
{
    public interface IOrderService
    {
        Task<OrderModel> GetOpenOrInProcessOrderAsync(Guid customerId);

        Task<OrderModel> GetOrderById(Guid orderId);

        Task<IEnumerable<OrderModel>> GetOrders(FilterOrderModel filterOrderModel = null);

        Task<OrderModel> EditOrder(OrderModel order);

        Task AddToOpenOrder(Guid customerId, GameModel product, short quantity);

        Task RemoveFromOrder(Guid customerId, string gameKey);

        Task<OrderModel> ChangeStatusToInProcess(Guid customerId);

        Task<OrderModel> ChangeStatusToClosed(Guid orderId);

        Task CancelOrdersWithTimeout();

        Task SetCancelledDate(Guid orderId, DateTime cancelledDate);
    }
}