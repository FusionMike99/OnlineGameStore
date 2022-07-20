using System.ComponentModel.DataAnnotations;

namespace OnlineGameStore.MVC.Models
{
    public class ShipperViewModel
    {
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        
        public string Phone { get; set; }
    }
}