using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlineGameStore.MVC.Models
{
    public class EditUserViewModel
    {
        [Required]
        [Display(Name = "Select Role")]
        public string Email { get; set; }
        
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }
        
        [Required]
        [Display(Name = "Select Role")]
        public string SelectedRole { get; set; }
        
        public SelectList Roles { get; set; }
    }
}