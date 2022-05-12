using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using OnlineGameStore.BLL.Entities;

namespace OnlineGameStore.MVC.Strategies.PaymentMethods
{
    public class VisaPaymentMethodStrategy : IPaymentMethodStrategy
    {
        public PaymentMethod PaymentMethod => PaymentMethod.Visa;

        public IActionResult ProcessPayment(Order order)
        {
            var viewDataDictionary = new ViewDataDictionary(
                new EmptyModelMetadataProvider(),
                new ModelStateDictionary());

            var viewResult = new ViewResult
            {
                ViewData = viewDataDictionary,
                ViewName = "Visa"
            };

            viewResult.ViewData["OrderId"] = order.Id;

            return viewResult;
        }
    }
}