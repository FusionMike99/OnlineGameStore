using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Models;
using OnlineGameStore.BLL.Models.General;

namespace OnlineGameStore.BLL.Repositories
{
    public interface IOrderRepository
    {
        Task<OrderModel> GetOpenOrInProcessOrderAsync(Guid customerId);

        Task<IEnumerable<OrderModel>> GetOrdersAsync(FilterOrderModel filterOrderModel = null);
        
        Task UpdateAsync(OrderModel orderModel);

        Task AddProductToOrderAsync(Guid customerId, GameModel product, short quantity);

        Task RemoveProductFromOrderAsync(Guid customerId, string gameKey);

        Task<OrderModel> ChangeStatusToInProcessAsync(Guid customerId);

        Task<OrderModel> ChangeStatusToClosedAsync(Guid orderId);

        Task<OrderModel> GetOrderByIdAsync(Guid orderId);

        Task CancelOrdersWithTimeoutAsync();

        Task SetCancelledDateAsync(Guid orderId, DateTime cancelledDate);
    }
}