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

        Task<IEnumerable<OrderEntity>> GetOrdersWithStatusAsync(OrderState orderState);

        Task AddProductToOrderAsync(Guid customerId, GameEntity product, short quantity);

        Task RemoveProductFromOrderAsync(Guid customerId, string gameKey);
        
        Task<OrderEntity> ChangeStatusToInProcessAsync(Guid customerId);

        Task<OrderEntity> ChangeStatusToClosedAsync(Guid orderId);

        Task<OrderEntity> GetOrderByIdAsync(Guid orderId);

        Task SetCancelledDateAsync(Guid orderId, DateTime cancelledDate);
    }
}