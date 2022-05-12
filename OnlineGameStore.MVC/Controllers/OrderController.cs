using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Services.Contracts;
using OnlineGameStore.MVC.Infrastructure;
using OnlineGameStore.MVC.Strategies.PaymentMethods;
using OnlineGameStore.MVC.Models;

namespace OnlineGameStore.MVC.Controllers
{
    [Route("orders")]
    public class OrderController : Controller
    {
        private readonly ICustomerIdAccessor _customerIdAccessor;
        private readonly IGameService _gameService;
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;
        private readonly IEnumerable<IPaymentMethodStrategy> _paymentMethodStrategies;

        public OrderController(IOrderService orderService,
            IGameService gameService,
            IMapper mapper,
            IEnumerable<IPaymentMethodStrategy> paymentMethodStrategies,
            ICustomerIdAccessor customerIdAccessor)
        {
            _orderService = orderService;
            _gameService = gameService;
            _paymentMethodStrategies = paymentMethodStrategies;
            _mapper = mapper;
            _customerIdAccessor = customerIdAccessor;
        }

        [HttpGet("{gameKey}/buy")]
        public IActionResult BuyProduct([FromRoute] string gameKey)
        {
            var game = _gameService.GetGameByKey(gameKey);

            if (game == null)
            {
                return NotFound("Game has not been found");
            }

            var customerId = _customerIdAccessor.GetCustomerId();

            _orderService.AddToOpenOrder(customerId, game, 1);

            return RedirectToAction(nameof(GetBasket));
        }

        [HttpGet("basket")]
        public IActionResult GetBasket()
        {
            var customerId = _customerIdAccessor.GetCustomerId();

            var order = _orderService.GetOpenOrder(customerId);

            return View("Basket", PrepareOrderViewModel(order));
        }

        [HttpGet("make")]
        public IActionResult Make()
        {
            var customerId = _customerIdAccessor.GetCustomerId();

            var order = _orderService.ChangeStatusToInProcess(customerId);

            return View(PrepareOrderViewModel(order));
        }

        [HttpGet("pay/{paymentMethod}")]
        public IActionResult Pay([FromRoute] PaymentMethod paymentMethod)
        {
            var paymentMethodStrategy = _paymentMethodStrategies.Single(s => s.PaymentMethod == paymentMethod);

            var customerId = _customerIdAccessor.GetCustomerId();

            var order = _orderService.GetOpenOrder(customerId);

            var result = paymentMethodStrategy.ProcessPayment(order);

            return result;
        }

        [HttpPost]
        [Route("{orderId:int}/pay", Name = "payOrder")]
        public IActionResult Pay([FromRoute] int orderId)
        {
            _orderService.ChangeStatusToClosed(orderId);

            return RedirectToAction("GetGames", "Game");
        }

        private OrderViewModel PrepareOrderViewModel(Order order)
        {
            var orderViewModel = _mapper.Map<OrderViewModel>(order);

            ViewData["GrandTotal"] = orderViewModel.Total.ToString("c");

            return orderViewModel;
        }
    }
}