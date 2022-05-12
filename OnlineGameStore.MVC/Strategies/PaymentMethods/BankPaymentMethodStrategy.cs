using System.Text;
using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.MVC.Models;

namespace OnlineGameStore.MVC.Strategies.PaymentMethods
{
    public class BankPaymentMethodStrategy : IPaymentMethodStrategy
    {
        private readonly IMapper _mapper;
        
        public PaymentMethod PaymentMethod => PaymentMethod.Bank;

        public BankPaymentMethodStrategy(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IActionResult ProcessPayment(Order order)
        {
            var orderViewModel = _mapper.Map<OrderViewModel>(order);

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var serializedGame = Encoding.Default.GetBytes(JsonSerializer.Serialize(orderViewModel, options));

            var fileContentResult = new FileContentResult(serializedGame, "application/txt")
            {
                FileDownloadName = $"invoice№{order.Id}.txt"
            };

            return fileContentResult;
        }
    }
}