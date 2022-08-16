using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using OnlineGameStore.MVC.Infrastructure;

namespace OnlineGameStore.MVC.Models
{
    public class EditPublisherViewModel
    {
        [UIHint("HiddenInput")]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Company name")]
        [MaxLength(40)]
        [BindProperty(BinderType = typeof(TrimmingModelBinder))]
        public string CompanyName { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Home page")]
        [Url]
        public string HomePage { get; set; }
        
        [Display(Name = "Contact name")]
        public string ContactName { get; set; }
        
        [Display(Name = "Contact title")]
        public string ContactTitle { get; set; }
        
        public string Address { get; set; }
        
        public string City { get; set; }
        
        [Display(Name = "Postal code")]
        public string PostalCode { get; set; }
        
        public string Region { get; set; }
        
        public string Country { get; set; }
        
        public string Phone { get; set; }
        
        public string Fax { get; set; }
    }
}