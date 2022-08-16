using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.DomainModels.Models;
using OnlineGameStore.DomainModels.Models.General;

namespace OnlineGameStore.BLL.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderModel> GetOpenOrInProcessOrderAsync(Guid customerId);

        Task<OrderModel> GetOrderByIdAsync(Guid orderId);

        Task<IEnumerable<OrderModel>> GetOrdersAsync(FilterOrderModel filterOrderModel = null);

        Task<OrderModel> EditOrderAsync(OrderModel order);

        Task AddToOpenOrderAsync(Guid customerId, GameModel product, short quantity);

        Task RemoveFromOrderAsync(Guid customerId, string gameKey);

        Task<OrderModel> ChangeStatusToInProcessAsync(Guid customerId);

        Task<OrderModel> ChangeStatusToClosedAsync(Guid orderId);

        Task CancelOrdersWithTimeoutAsync();

        Task SetCancelledDateAsync(Guid orderId, DateTime cancelledDate);
    }
}