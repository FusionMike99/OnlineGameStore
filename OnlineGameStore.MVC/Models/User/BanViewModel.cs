using System.ComponentModel.DataAnnotations;
using OnlineGameStore.BLL.Enums;

namespace OnlineGameStore.MVC.Models
{
    public class BanViewModel
    {
        [Required]
        public string UserName { get; set; }
        
        [Required]
        public BanPeriod? BanPeriod { get; set; }
        
        public string ReturnUrl { get; set; }
    }
}