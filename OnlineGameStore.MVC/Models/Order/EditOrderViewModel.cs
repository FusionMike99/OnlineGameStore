using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineGameStore.DomainModels.Enums;

namespace OnlineGameStore.MVC.Models
{
    public class EditOrderViewModel
    {
        [Display(Name = "Order Id")]
        public Guid OrderId { get; set; }

        [Display(Name = "Customer Id")]
        public Guid CustomerId { get; set; }
        
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }
        
        [Display(Name = "Cancelled Date")]
        public DateTime? CancelledDate { get; set; }
        
        [Display(Name = "Shipped Date")]
        public DateTime? ShippedDate { get; set; }
        
        [Required]
        [Display(Name = "Ship Name")]
        public string ShipName { get; set; }
        
        [Required]
        [DataType(DataType.Currency)]
        public decimal Freight { get; set; }
        
        [Required]
        [Display(Name = "Ship Address")]
        public string ShipAddress { get; set; }
        
        [Required]
        [Display(Name = "Ship City")]
        public string ShipCity { get; set; }
        
        [Required]
        [Display(Name = "Ship Region")]
        public string ShipRegion { get; set; }
        
        [Required]
        [Display(Name = "Ship Postal code")]
        public string ShipPostalCode { get; set; }
        
        [Required]
        [Display(Name = "Ship Country")]
        public string ShipCountry { get; set; }
        
        [Required]
        [Display(Name = "Ship Via")]
        public string ShipVia { get; set; }
        
        public SelectList Shippers { get; set; }

        [UIHint("HiddenInput")]
        public OrderState OrderState { get; set; }

        public IList<EditOrderDetailViewModel> OrderDetails { get; set; }
    }
}