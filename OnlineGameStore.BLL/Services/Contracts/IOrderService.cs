using System;
using System.Collections.Generic;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Models;

namespace OnlineGameStore.BLL.Services.Contracts
{
    public interface IOrderService
    {
        Order GetOpenOrder(string customerId);

        Order GetOrderById(int orderId);

        IEnumerable<Order> GetOrders(FilterOrderModel filterOrderModel = null);

        Order EditOrder(Order order);

        void AddToOpenOrder(string customerId, Game product, short quantity);

        void RemoveFromOrder(string customerId, string gameKey);

        Order ChangeStatusToInProcess(string customerId);

        Order ChangeStatusToClosed(int orderId);

        void CancelOrdersWithTimeout();

        void SetCancelledDate(int orderId, DateTime cancelledDate);
    }
}