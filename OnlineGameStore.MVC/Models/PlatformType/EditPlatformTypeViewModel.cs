using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using OnlineGameStore.MVC.Infrastructure;

namespace OnlineGameStore.MVC.Models
{
    public class EditPlatformTypeViewModel
    {
        [UIHint("HiddenInput")]
        public Guid Id { get; set; }

        [Required]
        [BindProperty(BinderType = typeof(TrimmingModelBinder))]
        public string Type { get; set; }
    }
}