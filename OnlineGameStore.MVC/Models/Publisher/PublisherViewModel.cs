using System.ComponentModel.DataAnnotations;
using OnlineGameStore.BLL.Enums;

namespace OnlineGameStore.MVC.Models
{
    public class PublisherViewModel
    {
        [UIHint("HiddenInput")]
        public int Id { get; set; }

        [Display(Name = "Company name")]
        public string CompanyName { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DataType(DataType.Url)]
        [Display(Name = "Home page")]
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
        
        public DatabaseEntity DatabaseEntity { get; set; }
    }
}