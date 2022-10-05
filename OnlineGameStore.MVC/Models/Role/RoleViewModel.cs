using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineGameStore.MVC.Models
{
    public class RoleViewModel
    {
        [UIHint("Hidden")]
        public Guid Id { get; set; }
        
        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}