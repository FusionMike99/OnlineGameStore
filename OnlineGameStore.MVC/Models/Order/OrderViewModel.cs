using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using OnlineGameStore.BLL.Enums;

namespace OnlineGameStore.MVC.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Customer")]
        public string CustomerId { get; set; }

        [Display(Name = "Order Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d.M.yyyy}")]
        public DateTime OrderDate { get; set; }
        
        [Display(Name = "Cancelled Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d.M.yyyy}")]
        public DateTime? CancelledDate { get; set; }
        
        [Display(Name = "Shipped Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d.M.yyyy}")]
        public DateTime? ShippedDate { get; set; }
        
        [DataType(DataType.Currency)]
        public decimal Freight { get; set; }
        
        [Display(Name = "Ship Name")]
        public string ShipName { get; set; }
        
        [Display(Name = "Ship Address")]
        public string ShipAddress { get; set; }
        
        [Display(Name = "Ship City")]
        public string ShipCity { get; set; }
        
        [Display(Name = "Ship Region")]
        public string ShipRegion { get; set; }
        
        [Display(Name = "Ship Postal code")]
        public string ShipPostalCode { get; set; }
        
        [Display(Name = "Ship Country")]
        public string ShipCountry { get; set; }
        
        [Display(Name = "Ship Via")]
        public string ShipVia { get; set; }
        
        [Display(Name = "Order Status")]
        public OrderState OrderState { get; set; }

        [DataType(DataType.Currency)]
        public decimal Total { get; set; }

        public ICollection<OrderDetailViewModel> OrderDetails { get; set; }
        
        [JsonIgnore]
        public bool EnableModification { get; set; }
    }
}