using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Services.Contracts;

namespace OnlineGameStore.MVC.Strategies.PaymentMethods
{
    public class VisaPaymentMethodStrategy : IPaymentMethodStrategy
    {
        private readonly IOrderService _orderService;
        
        public PaymentMethod PaymentMethod => PaymentMethod.Visa;

        public VisaPaymentMethodStrategy(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult PaymentProcess(Order order)
        {
            const string viewName = "Visa";
            const string orderId = "OrderId";
            const int minutes = 1;
            
            var viewDataDictionary = new ViewDataDictionary(
                new EmptyModelMetadataProvider(),
                new ModelStateDictionary());

            var viewResult = new ViewResult
            {
                ViewData = viewDataDictionary,
                ViewName = viewName
            };

            viewResult.ViewData[orderId] = order.Id;
            
            var cancelledDate = DateTime.UtcNow.Add(TimeSpan.FromMinutes(minutes));
            
            _orderService.SetCancelledDate(order.Id, cancelledDate);

            return viewResult;
        }
    }
}