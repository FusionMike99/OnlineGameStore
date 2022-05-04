using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineGameStore.MVC.Services.Contracts;
using OnlineGameStore.MVC.Models;
using System.Collections.Generic;

namespace OnlineGameStore.MVC.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IMapper _mapper;

        public CartController(ICartService cartService,
            IMapper mapper)
        {
            _cartService = cartService;
            _mapper = mapper;
        }

        [HttpGet("basket")]
        public IActionResult GetBasket()
        {
            var orderDetails = _cartService.OrderDetails;

            var orderDetailsViewModel = _mapper.Map<IEnumerable<OrderDetailViewModel>>(orderDetails);

            ViewData["GrandTotal"] = _cartService.ComputeTotalValue().ToString("c");

            return View("Index", orderDetailsViewModel);
        }
    }
}
