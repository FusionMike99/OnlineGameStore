using System;

namespace OnlineGameStore.BLL.Entities
{
    public class OrderDetail : BaseEntity
    {
        public string GameKey { get; set; }

        public GameEntity Product { get; set; }

        public Guid OrderId { get; set; }

        public OrderEntity Order { get; set; }

        public decimal Price { get; set; }

        public short Quantity { get; set; }

        public float Discount { get; set; }
    }
}