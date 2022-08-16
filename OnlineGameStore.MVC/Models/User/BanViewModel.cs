using System.ComponentModel.DataAnnotations;
using OnlineGameStore.DomainModels.Enums;

namespace OnlineGameStore.MVC.Models
{
    public class BanViewModel
    {
        [Required]
        public string UserName { get; set; }
        
        [Required]
        [Display(Name = "Ban period")]
        public BanPeriod? BanPeriod { get; set; }
        
        public string ReturnUrl { get; set; }
    }
}