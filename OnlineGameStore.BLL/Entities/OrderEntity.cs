using System;
using System.Collections.Generic;
using OnlineGameStore.BLL.Enums;

namespace OnlineGameStore.BLL.Entities
{
    public class OrderEntity : BaseEntity
    {
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

        public Guid CustomerId { get; set; }

        public OrderState OrderState { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}