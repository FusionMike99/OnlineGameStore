using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using OnlineGameStore.MVC.Infrastructure;

namespace OnlineGameStore.MVC.Models
{
    public class EditPlatformTypeViewModel
    {
        [UIHint("HiddenInput")]
        [BindProperty(BinderType = typeof(TrimmingModelBinder))]
        public int Id { get; set; }

        [Required]
        public string Type { get; set; }
    }
}
