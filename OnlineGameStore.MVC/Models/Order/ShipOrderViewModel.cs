using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineGameStore.DomainModels.Enums;

namespace OnlineGameStore.MVC.Models
{
    public class ShipOrderViewModel
    {
        [Required]
        [Display(Name = "Ship Name")]
        public string ShipName { get; set; }
        
        [DataType(DataType.Currency)]
        [Editable(false)]
        public decimal Freight { get; }
        
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
    }
}