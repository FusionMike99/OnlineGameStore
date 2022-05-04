using OnlineGameStore.BLL.Entities;
using OnlineGameStore.MVC.Services.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace OnlineGameStore.MVC.Services
{
    public class CartService : ICartService
    {
        public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        public virtual void AddItem(Game product, short quantity)
        {
            var cartLine = OrderDetails.FirstOrDefault(od => od.ProductId == product.Id);

            if (cartLine == null)
            {
                OrderDetails.Add(new OrderDetail
                {
                    ProductId = product.Id,
                    Product = product,
                    Price = product.Price,
                    Quantity = quantity
                });
            }
            else
            {
                cartLine.Quantity += quantity;
            }
        }

        public virtual void Clear()
        {
            OrderDetails.Clear();
        }

        public virtual decimal ComputeTotalValue()
        {
            var total = OrderDetails.Sum(e => e.Price * e.Quantity * (decimal)(1 - e.Discount));

            return total;
        }

        public virtual void RemoveItem(Game product)
        {
            OrderDetails.RemoveAll(od => od.ProductId == product.Id);
        }
    }
}
