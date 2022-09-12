using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlineGameStore.MVC.Models
{
    public class EditUserViewModel
    {
        [UIHint("HiddenInput")]
        public Guid Id { get; set; }
        
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }
        
        [Required]
        [Display(Name = "Select Role")]
        public string SelectedRole { get; set; }
        
        public SelectList Roles { get; set; }
        
        [Display(Name = "Select Publisher")]
        public Guid? SelectedPublisher { get; set; }
        
        public SelectList Publishers { get; set; }
    }
}