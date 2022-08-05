﻿using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Services.Contracts;
using OnlineGameStore.MVC.Models;

namespace OnlineGameStore.MVC.Strategies.PaymentMethods
{
    public class TerminalIBoxPaymentMethodStrategy : IPaymentMethodStrategy
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public PaymentMethod PaymentMethod => PaymentMethod.IBoxTerminal;

        public TerminalIBoxPaymentMethodStrategy(IOrderService orderService,
            IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        public IActionResult PaymentProcess(Order order)
        {
            const string viewName = "IBox";
            const int minutes = 2;
            
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
                ViewName = viewName
            };
            
            var cancelledDate = DateTime.UtcNow.Add(TimeSpan.FromMinutes(minutes));
            
            _orderService.SetCancelledDate(order.Id.ToString(), cancelledDate);
            
            return viewResult;
        }
    }
}