using System;
using OnlineGameStore.BLL.Entities;

namespace OnlineGameStore.BLL.Services.Contracts
{
    public interface IOrderService
    {
        Order GetOpenOrder(int customerId);

        void AddToOpenOrder(int customerId, Game product, short quantity);

        Order ChangeStatusToInProcess(int customerId);

        Order ChangeStatusToClosed(int orderId);

        void CancelOrdersWithTimeout(TimeSpan timeout);
    }
}