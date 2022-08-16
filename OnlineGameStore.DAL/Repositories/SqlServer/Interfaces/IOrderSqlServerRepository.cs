using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.DAL.Entities;
using OnlineGameStore.DomainModels.Enums;
using OnlineGameStore.DomainModels.Models;

namespace OnlineGameStore.DAL.Repositories.SqlServer.Interfaces
{
    public interface IOrderSqlServerRepository : ISqlServerRepository<OrderEntity>
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