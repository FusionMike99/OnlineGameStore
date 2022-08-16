using System;

namespace OnlineGameStore.DomainModels.Models.General
{
    public class OrderDetailModel : BaseModel
    {
        public string GameKey { get; set; }

        public GameModel Product { get; set; }

        public Guid OrderId { get; set; }

        public OrderModel Order { get; set; }

        public decimal Price { get; set; }

        public short Quantity { get; set; }

        public float Discount { get; set; }
    }
}