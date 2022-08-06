using System;

namespace OnlineGameStore.BLL.Entities
{
    public class OrderDetail : BaseEntity
    {
        public string GameKey { get; set; }

        public Game Product { get; set; }

        public Guid OrderId { get; set; }

        public Order Order { get; set; }

        public decimal Price { get; set; }

        public short Quantity { get; set; }

        public float Discount { get; set; }
    }
}