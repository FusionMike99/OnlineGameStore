using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Enums;
using OnlineGameStore.BLL.Models;

namespace OnlineGameStore.BLL.Repositories.GameStore
{
    public interface IGameStoreOrderRepository : IGameStoreGenericRepository<OrderEntity>
    {
        Task<OrderEntity> GetOpenOrInProcessOrderAsync(Guid customerId);

        Task<IEnumerable<OrderEntity>> GetOrdersAsync(FilterOrderModel filterOrderModel = null);

        Task<IEnumerable<OrderEntity>> GetOrdersWithStatus(OrderState orderState);

        Task AddProductToOrder(Guid customerId, GameEntity product, short quantity);

        Task RemoveProductFromOrder(Guid customerId, string gameKey);
        
        Task<OrderEntity> ChangeStatusToInProcess(Guid customerId);

        Task<OrderEntity> ChangeStatusToClosed(Guid orderId);

        Task<OrderEntity> GetOrderById(Guid orderId);

        Task SetCancelledDate(Guid orderId, DateTime cancelledDate);
    }
}