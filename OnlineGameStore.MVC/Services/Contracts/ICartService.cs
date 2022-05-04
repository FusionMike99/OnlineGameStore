using OnlineGameStore.BLL.Entities;
using System.Collections.Generic;

namespace OnlineGameStore.MVC.Services.Contracts
{
    public interface ICartService
    {
        public List<OrderDetail> OrderDetails { get; set; }

        public void AddItem(Game product, short quantity);

        public void RemoveItem(Game product);

        public decimal ComputeTotalValue();

        public void Clear();
    }
}
