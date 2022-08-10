using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Enums;
using OnlineGameStore.BLL.Models;

namespace OnlineGameStore.BLL.Repositories.GameStore
{
    public interface IGameStoreOrderRepository : IGameStoreGenericRepository<Order>
    {
        Task<Order> GetOpenOrInProcessOrderAsync(Guid customerId);

        Task<IEnumerable<Order>> GetOrdersAsync(FilterOrderModel filterOrderModel = null);

        Task<IEnumerable<Order>> GetOrdersWithStatus(OrderState orderState);

        Task AddProductToOrder(Guid customerId, Game product, short quantity);

        Task RemoveProductFromOrder(Guid customerId, string gameKey);
        
        Task<Order> ChangeStatusToInProcess(Guid customerId);

        Task<Order> ChangeStatusToClosed(Guid orderId);

        Task<Order> GetOrderById(Guid orderId);

        Task SetCancelledDate(Guid orderId, DateTime cancelledDate);
    }
}