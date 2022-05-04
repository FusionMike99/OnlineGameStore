using Microsoft.AspNetCore.Mvc;
using OnlineGameStore.MVC.Services.Contracts;
using System.Linq;

namespace OnlineGameStore.MVC.Components
{
    public class CartSummaryViewComponent : ViewComponent
    {
        private readonly ICartService _cartService;

        public CartSummaryViewComponent(ICartService cartService)
        {
            _cartService = cartService;
        }

        public IViewComponentResult Invoke()
        {
            var totalGames = _cartService.OrderDetails.Sum(od => od.Quantity);

            return View(totalGames);
        }
    }
}
