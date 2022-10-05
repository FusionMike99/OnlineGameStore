using System;

namespace OnlineGameStore.DomainModels.Models
{
    public class ShipOrderModel
    {
        public Guid Id { get; set; }
        
        public string ShipName { get; set; }
        
        public decimal Freight { get; set; }
        
        public string ShipAddress { get; set; }
        
        public string ShipCity { get; set; }
        
        public string ShipRegion { get; set; }
        
        public string ShipPostalCode { get; set; }
        
        public string ShipCountry { get; set; }
        
        public string ShipVia { get; set; }
    }
}