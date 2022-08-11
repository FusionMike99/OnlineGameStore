using Microsoft.AspNetCore.Mvc;
using OnlineGameStore.BLL.Models.General;

namespace OnlineGameStore.MVC.Strategies.PaymentMethods
{
    public interface IPaymentMethodStrategy
    {
        PaymentMethod PaymentMethod { get; }

        IActionResult PaymentProcess(OrderModel order);
    }
}