using System;
using System.Collections.Generic;

namespace OnlineGameStore.BLL.Entities
{
    public class Order : IBaseEntity<int>
    {
        public int Id { get; set; }
        
        public DateTime OrderDate { get; set; }
        
        public DateTime? CancelledDate { get; set; }
        
        public DateTime? ShippedDate { get; set; }
        
        public decimal Freight { get; set; }
        
        public string ShipName { get; set; }
        
        public string ShipAddress { get; set; }
        
        public string ShipCity { get; set; }
        
        public string ShipRegion { get; set; }
        
        public string ShipPostalCode { get; set; }
        
        public string ShipCountry { get; set; }
        
        public string ShipVia { get; set; }

        public string CustomerId { get; set; }

        public int OrderStatusId { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}