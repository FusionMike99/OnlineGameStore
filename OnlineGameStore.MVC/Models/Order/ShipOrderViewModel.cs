using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlineGameStore.MVC.Models
{
    public class ShipOrderViewModel
    {
        [UIHint("HiddenInput")]
        public Guid Id { get; set; }

        [UIHint("HiddenInput")]
        public Guid CustomerId { get; set; }

        [UIHint("HiddenInput")]
        public DateTime OrderDate { get; set; }
        
        [UIHint("HiddenInput")]
        public DateTime? CancelledDate { get; set; }
        
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
        
        [Display(Name = "Shipped Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        [Editable(false)]
        public DateTime? ShippedDate { get; } = DateTime.UtcNow.AddDays(3);
        
        [UIHint("HiddenInput")]
        public int OrderStatusId { get; set; }
    }
}