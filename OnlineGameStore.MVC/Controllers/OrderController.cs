using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Entities.Northwind;
using OnlineGameStore.BLL.Models;
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
        public IActionResult BuyProduct([FromRoute] string gameKey)
        {
            const int quantity = 1;
            
            var game = _gameService.GetGameByKey(gameKey);

            if (game == null)
            {
                return NotFound();
            }

            var customerId = _customerIdAccessor.GetCustomerId();

            _orderService.AddToOpenOrder(customerId, game, quantity);

            return RedirectToAction(nameof(GetBasket));
        }

        [HttpGet("basket")]
        public IActionResult GetBasket()
        {
            var customerId = _customerIdAccessor.GetCustomerId();

            var order = _orderService.GetOpenOrder(customerId);

            var orderViewModel = PrepareOrderViewModel(order, true);

            return View("Basket", orderViewModel);
        }
        
        [HttpPost("basket/remove/{gameKey}")]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveFromBasket(string gameKey)
        {
            if (string.IsNullOrWhiteSpace(gameKey))
            {
                return BadRequest();
            }
            
            var customerId = _customerIdAccessor.GetCustomerId();

            _orderService.RemoveFromOrder(customerId, gameKey);

            return RedirectToAction(nameof(GetBasket));
        }
        
        [HttpGet("orders/history")]
        public IActionResult GetOrders(FilterOrderViewModel filterOrderViewModel = null)
        {
            var filterOrderModel = _mapper.Map<FilterOrderModel>(filterOrderViewModel);
            
            var orders = _orderService.GetOrders(filterOrderModel);

            var ordersViewModel = _mapper.Map<IEnumerable<OrderViewModel>>(orders);

            var orderListViewModel = new OrderListViewModel
            {
                Orders = ordersViewModel,
                FilterOrderViewModel = filterOrderViewModel
            };

            return View("Index", orderListViewModel);
        }
        
        [HttpGet("orders/ship/{orderId}")]
        public IActionResult Ship(string orderId)
        {
            var order = _orderService.GetOrderById(orderId);

            if (order == null)
            {
                return NotFound();
            }

            var editOrderViewModel = _mapper.Map<ShipOrderViewModel>(order);

            ConfigureShipOrderViewModel(editOrderViewModel);

            return View(editOrderViewModel);
        }

        [HttpPost("orders/ship/{orderId}")]
        [ValidateAntiForgeryToken]
        public IActionResult Ship(string orderId, [FromForm] ShipOrderViewModel order)
        {
            if (!ModelState.IsValid)
            {
                ConfigureShipOrderViewModel(order);

                return View(order);
            }

            order.Id = orderId;
            var mappedOrder = _mapper.Map<Order>(order);

            _orderService.EditOrder(mappedOrder);

            return RedirectToAction(nameof(Make));
        }

        private void ConfigureShipOrderViewModel(ShipOrderViewModel shipOrderViewModel)
        {
            shipOrderViewModel.Shippers = new SelectList(_shipperService.GetAllShippers(),
                nameof(NorthwindShipper.CompanyName),
                nameof(NorthwindShipper.CompanyName));
        }

        [HttpGet("orders/make")]
        public IActionResult Make()
        {
            var customerId = _customerIdAccessor.GetCustomerId();

            var order = _orderService.ChangeStatusToInProcess(customerId);

            var orderViewModel = PrepareOrderViewModel(order);

            return View(orderViewModel);
        }

        [HttpGet("orders/{orderId}/pay")]
        public IActionResult Pay(string orderId, PaymentMethod paymentMethod)
        {
            var paymentMethodStrategy = _paymentMethodStrategies.Single(s => s.PaymentMethod == paymentMethod);

            var order = _orderService.GetOrderById(orderId);

            var result = paymentMethodStrategy.PaymentProcess(order);

            return result;
        }

        [HttpPost("orders/{orderId}/pay")]
        [ValidateAntiForgeryToken]
        public IActionResult Pay([FromRoute] string orderId)
        {
            _orderService.ChangeStatusToClosed(orderId);

            return RedirectToAction(nameof(GameController.GetGames), "Game");
        }

        private OrderViewModel PrepareOrderViewModel(Order order, bool enableModification = false)
        {
            var orderViewModel = _mapper.Map<OrderViewModel>(order);

            orderViewModel.EnableModification = enableModification;

            return orderViewModel;
        }
    }
}