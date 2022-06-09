using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OnlineGameStore.MVC.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public int CustomerId { get; set; }

        public decimal Total { get; set; }

        public ICollection<OrderDetailViewModel> OrderDetails { get; set; }
        
        [JsonIgnore]
        public bool EnableModification { get; set; }
    }
}