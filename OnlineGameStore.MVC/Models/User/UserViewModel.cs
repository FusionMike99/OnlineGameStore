using System.ComponentModel.DataAnnotations;

namespace OnlineGameStore.MVC.Models
{
    public class UserViewModel
    {
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        [Display(Name = "User name")]
        public string UserName { get; set; }
    }
}