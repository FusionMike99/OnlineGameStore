using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using OnlineGameStore.MVC.Infrastructure;

namespace OnlineGameStore.MVC.Models
{
    public class EditPublisherViewModel
    {
        [UIHint("HiddenInput")]
        public int Id { get; set; }

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
    }
}
