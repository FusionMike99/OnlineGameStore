using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineGameStore.BLL.Entities;
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

            var orderViewModel = PrepareOrderViewModel(order);

            orderViewModel.EnableModification = true;

            return View("Basket", orderViewModel);
        }
        
        [HttpPost("basket/remove/{productId:int}")]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveFromBasket(int? productId)
        {
            if (!productId.HasValue)
            {
                return BadRequest();
            }
            
            var customerId = _customerIdAccessor.GetCustomerId();

            _orderService.RemoveFromOrder(customerId, productId.Value);

            return RedirectToAction(nameof(GetBasket));
        }

        [HttpGet("orders/make")]
        public IActionResult Make()
        {
            var customerId = _customerIdAccessor.GetCustomerId();

            var order = _orderService.ChangeStatusToInProcess(customerId);

            var orderViewModel = PrepareOrderViewModel(order);

            orderViewModel.EnableModification = false;

            return View(orderViewModel);
        }

        [HttpGet("orders/{orderId:int}/pay")]
        public IActionResult Pay(int orderId, PaymentMethod paymentMethod)
        {
            var paymentMethodStrategy = _paymentMethodStrategies.Single(s => s.PaymentMethod == paymentMethod);

            var order = _orderService.GetOrderById(orderId);

            var result = paymentMethodStrategy.PaymentProcess(order);

            return result;
        }

        [HttpPost("orders/{orderId:int}/pay")]
        [ValidateAntiForgeryToken]
        public IActionResult Pay([FromRoute] int orderId)
        {
            _orderService.ChangeStatusToClosed(orderId);

            return RedirectToAction(nameof(GameController.GetGames), "Game");
        }

        private OrderViewModel PrepareOrderViewModel(Order order)
        {
            var orderViewModel = _mapper.Map<OrderViewModel>(order);

            return orderViewModel;
        }
    }
}