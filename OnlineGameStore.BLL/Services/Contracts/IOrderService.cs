using System;
using OnlineGameStore.BLL.Entities;

namespace OnlineGameStore.BLL.Services.Contracts
{
    public interface IOrderService
    {
        Order GetOpenOrder(int customerId);

        Order GetOrderById(int orderId);

        void AddToOpenOrder(int customerId, Game product, short quantity);

        void RemoveFromOrder(int customerId, int productId);

        Order ChangeStatusToInProcess(int customerId);

        Order ChangeStatusToClosed(int orderId);

        void CancelOrdersWithTimeout();

        void SetCancelledDate(int orderId, DateTime cancelledDate);
    }
}