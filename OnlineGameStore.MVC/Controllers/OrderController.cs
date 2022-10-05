﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineGameStore.BLL.Services.Interfaces;
using OnlineGameStore.DAL.Entities.Northwind;
using OnlineGameStore.DomainModels.Constants;
using OnlineGameStore.DomainModels.Enums;
using OnlineGameStore.DomainModels.Models;
using OnlineGameStore.DomainModels.Models.General;
using OnlineGameStore.MVC.Infrastructure;
using OnlineGameStore.MVC.Models;
using OnlineGameStore.MVC.Strategies.PaymentMethods;

namespace OnlineGameStore.MVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly ICustomerIdAccessor _customerIdAccessor;
        private readonly IGameService _gameService;
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;
        private readonly IShipperService _shipperService;
        private readonly IEnumerable<IPaymentMethodStrategy> _paymentMethodStrategies;

        public OrderController(IOrderService orderService,
            IGameService gameService,
            IShipperService shipperService,
            IMapper mapper,
            IEnumerable<IPaymentMethodStrategy> paymentMethodStrategies,
            ICustomerIdAccessor customerIdAccessor)
        {
            _orderService = orderService;
            _gameService = gameService;
            _shipperService = shipperService;
            _paymentMethodStrategies = paymentMethodStrategies;
            _mapper = mapper;
            _customerIdAccessor = customerIdAccessor;
        }

        [HttpGet("games/{gameKey}/buy")]
        public async Task<IActionResult> BuyProduct([FromRoute] string gameKey)
        {
            const int quantity = 1;
            
            var game = await _gameService.GetGameByKeyAsync(gameKey);
            if (game == null)
            {
                return NotFound();
            }

            var customerId = _customerIdAccessor.GetCustomerId();
            await _orderService.AddToOpenOrderAsync(customerId, game, quantity);

            return RedirectToAction(nameof(GetBasket));
        }

        [HttpGet("basket")]
        public async Task<IActionResult> GetBasket()
        {
            var customerId = _customerIdAccessor.GetCustomerId();

            var order = await _orderService.GetOpenOrInProcessOrderAsync(customerId);

            var orderViewModel = PrepareOrderViewModel(order, enableModification: true);

            return View("Basket", orderViewModel);
        }
        
        [HttpPost("basket/remove/{gameKey}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveFromBasket(string gameKey)
        {
            if (string.IsNullOrWhiteSpace(gameKey))
            {
                return BadRequest();
            }
            
            var customerId = _customerIdAccessor.GetCustomerId();

            await _orderService.RemoveFromOrderAsync(customerId, gameKey);

            return RedirectToAction(nameof(GetBasket));
        }

        [HttpGet("orders")]
        [AuthorizeByRoles(Permissions.ManagerPermission)]
        public async Task<IActionResult> GetOrders(FilterOrderViewModel filterOrderViewModel = default)
        {
            filterOrderViewModel!.MinDate ??= DateTime.UtcNow.AddDays(-30);
            var filterOrderModel = _mapper.Map<FilterOrderModel>(filterOrderViewModel);
            filterOrderModel.DatabaseEntity = DatabaseEntity.GameStore;
            
            var orders = await _orderService.GetOrdersAsync(filterOrderModel);
            var ordersViewModel = _mapper.Map<IEnumerable<OrderViewModel>>(orders);
            var orderListViewModel = new OrderListViewModel
            {
                Orders = ordersViewModel,
                FilterOrderViewModel = filterOrderViewModel
            };

            return View("Orders", orderListViewModel);
        }

        [HttpGet("orders/{orderId:guid}/change-status/shipped")]
        [AuthorizeByRoles(Permissions.ManagerPermission)]
        public async Task<IActionResult> ChangeStatusToShipped(Guid orderId)
        {
            await _orderService.ChangeStatusToShippedAsync(orderId);

            return RedirectToAction("GetOrders");
        }

        [HttpGet("orders/history")]
        [AuthorizeByRoles(Permissions.ManagerPermission)]
        public async Task<IActionResult> GetOrdersHistory(FilterOrderViewModel filterOrderViewModel = default)
        {
            filterOrderViewModel!.MaxDate ??= DateTime.UtcNow.AddDays(-30);
            var filterOrderModel = _mapper.Map<FilterOrderModel>(filterOrderViewModel);
            filterOrderModel.DatabaseEntity = DatabaseEntity.All;
            
            var orders = await _orderService.GetOrdersAsync(filterOrderModel);
            var ordersViewModel = _mapper.Map<IEnumerable<OrderViewModel>>(orders);
            var orderListViewModel = new OrderListViewModel
            {
                Orders = ordersViewModel,
                FilterOrderViewModel = filterOrderViewModel
            };

            return View("OrdersHistory", orderListViewModel);
        }

        [HttpGet("orders/ship/{orderId:guid}")]
        public async Task<IActionResult> Ship(Guid orderId)
        {
            var shipOrderViewModel = new ShipOrderViewModel();
            await ConfigureShipOrderViewModel(shipOrderViewModel);

            return View(shipOrderViewModel);
        }

        [HttpPost("orders/ship/{orderId:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Ship(Guid orderId, [FromForm] ShipOrderViewModel shipOrderViewModel)
        {
            if (!ModelState.IsValid)
            {
                await ConfigureShipOrderViewModel(shipOrderViewModel);

                return View(shipOrderViewModel);
            }

            var shipOrderModel = _mapper.Map<ShipOrderModel>(shipOrderViewModel);
            shipOrderModel.Id = orderId;
            await _orderService.ShipOrderAsync(shipOrderModel);

            return RedirectToAction(nameof(Make));
        }
        
        [HttpGet("orders/update/{orderId:guid}")]
        [AuthorizeByRoles(Permissions.ManagerPermission)]
        public async Task<IActionResult> Update([FromRoute] Guid orderId)
        {
            var order = await _orderService.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }

            var editOrderViewModel = _mapper.Map<EditOrderViewModel>(order);
            await ConfigureEditOrderViewModel(editOrderViewModel);

            return View(editOrderViewModel);
        }
        
        [HttpPost("orders/update/{orderId:guid}")]
        [AuthorizeByRoles(Permissions.PublisherPermission)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Guid orderId, [FromForm] EditOrderViewModel order)
        {
            if (!ModelState.IsValid)
            {
                await ConfigureEditOrderViewModel(order);

                return View(order);
            }

            if (orderId != order.OrderId)
            {
                return BadRequest();
            }

            var mappedOrder = _mapper.Map<OrderModel>(order);
            await _orderService.EditOrderAsync(mappedOrder);

            return RedirectToAction("GetOrders");
        }

        [HttpGet("orders/make")]
        public async Task<IActionResult> Make()
        {
            var customerId = _customerIdAccessor.GetCustomerId();
            var order = await _orderService.ChangeStatusToInProcessAsync(customerId);
            var orderViewModel = PrepareOrderViewModel(order);

            return View(orderViewModel);
        }

        [HttpGet("orders/{orderId:guid}/pay")]
        public async Task<IActionResult> Pay(Guid orderId, PaymentMethod paymentMethod)
        {
            var paymentMethodStrategy = _paymentMethodStrategies.Single(s => s.PaymentMethod == paymentMethod);
            var order = await _orderService.GetOrderByIdAsync(orderId);
            var result = paymentMethodStrategy.PaymentProcess(order);

            return result;
        }

        [HttpPost("orders/{orderId:guid}/pay")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Pay([FromRoute] Guid orderId)
        {
            await _orderService.ChangeStatusToClosedAsync(orderId);

            return RedirectToAction(nameof(GameController.GetGames), "Game");
        }

        private OrderViewModel PrepareOrderViewModel(OrderModel order, bool enableModification = false)
        {
            var orderViewModel = _mapper.Map<OrderViewModel>(order);

            orderViewModel.EnableModification = enableModification;

            return orderViewModel;
        }

        private async Task ConfigureShipOrderViewModel(ShipOrderViewModel shipOrderViewModel)
        {
            var shippers = await _shipperService.GetAllShippersAsync();

            shipOrderViewModel.Shippers = new SelectList(shippers, nameof(ShipperEntity.CompanyName),
                nameof(ShipperEntity.CompanyName));
        }

        private async Task ConfigureEditOrderViewModel(EditOrderViewModel editOrderViewModel)
        {
            var shippers = await _shipperService.GetAllShippersAsync();

            editOrderViewModel.Shippers = new SelectList(shippers, nameof(ShipperEntity.CompanyName),
                nameof(ShipperEntity.CompanyName));
        }
    }
}