using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.MVC.Models;

namespace OnlineGameStore.MVC.Strategies.PaymentMethods
{
    public class TerminalIBoxPaymentMethodStrategy : IPaymentMethodStrategy
    {
        private readonly IMapper _mapper;

        public PaymentMethod PaymentMethod => PaymentMethod.IBoxTerminal;

        public TerminalIBoxPaymentMethodStrategy(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IActionResult ProcessPayment(Order order)
        {
            var orderViewModel = _mapper.Map<OrderViewModel>(order);

            var viewDataDictionary = new ViewDataDictionary(
                new EmptyModelMetadataProvider(),
                new ModelStateDictionary())
            {
                Model = orderViewModel
            };

            var viewResult = new ViewResult
            {
                ViewData = viewDataDictionary,
                ViewName = "IBox"
            };

            return viewResult;
        }
    }
}