using Microsoft.AspNetCore.Mvc;
using OnlineGameStore.BLL.Entities;

namespace OnlineGameStore.MVC.Strategies.PaymentMethods
{
    public interface IPaymentMethodStrategy
    {
        PaymentMethod PaymentMethod { get; }

        IActionResult PaymentProcess(Order order);
    }
}