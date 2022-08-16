using System;
using System.Text;
using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineGameStore.BLL.Services.Interfaces;
using OnlineGameStore.DomainModels.Models.General;
using OnlineGameStore.MVC.Models;

namespace OnlineGameStore.MVC.Strategies.PaymentMethods
{
    public class BankPaymentMethodStrategy : IPaymentMethodStrategy
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        
        public PaymentMethod PaymentMethod => PaymentMethod.Bank;

        public BankPaymentMethodStrategy(IOrderService orderService,
            IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        public IActionResult PaymentProcess(OrderModel order)
        {
            const int minutes = 3;
            
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
            
            var cancelledDate = DateTime.UtcNow.Add(TimeSpan.FromMinutes(minutes));
            
            _orderService.SetCancelledDateAsync(order.Id, cancelledDate);
            
            return fileContentResult;
        }
    }
}