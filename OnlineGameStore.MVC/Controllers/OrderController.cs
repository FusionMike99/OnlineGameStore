using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineGameStore.BLL.Entities.Northwind;
using OnlineGameStore.BLL.Models;
using OnlineGameStore.BLL.Models.General;
using OnlineGameStore.BLL.Services.Contracts;
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
        
        [HttpGet("orders/history")]
        public async Task<IActionResult> GetOrders(FilterOrderViewModel filterOrderViewModel = null)
        {
            var filterOrderModel = _mapper.Map<FilterOrderModel>(filterOrderViewModel);
            
            var orders = await _orderService.GetOrdersAsync(filterOrderModel);

            var ordersViewModel = _mapper.Map<IEnumerable<OrderViewModel>>(orders);

            var orderListViewModel = new OrderListViewModel
            {
                Orders = ordersViewModel,
                FilterOrderViewModel = filterOrderViewModel
            };

            return View("Index", orderListViewModel);
        }
        
        [HttpGet("orders/ship/{orderId:guid}")]
        public async Task<IActionResult> Ship(Guid orderId)
        {
            var order = await _orderService.GetOrderByIdAsync(orderId);

            if (order == null)
            {
                return NotFound();
            }

            var editOrderViewModel = _mapper.Map<ShipOrderViewModel>(order);

            await ConfigureShipOrderViewModel(editOrderViewModel);

            return View(editOrderViewModel);
        }

        [HttpPost("orders/ship/{orderId:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Ship(Guid orderId, [FromForm] ShipOrderViewModel order)
        {
            if (!ModelState.IsValid)
            {
                await ConfigureShipOrderViewModel(order);

                return View(order);
            }

            order.Id = orderId;
            var mappedOrder = _mapper.Map<OrderModel>(order);

            await _orderService.EditOrderAsync(mappedOrder);

            return RedirectToAction(nameof(Make));
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
    }
}