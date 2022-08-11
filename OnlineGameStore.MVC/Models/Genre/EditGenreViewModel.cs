using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineGameStore.MVC.Infrastructure;

namespace OnlineGameStore.MVC.Models
{
    public class EditGenreViewModel
    {
        [UIHint("HiddenInput")]
        public Guid Id { get; set; }

        [Required]
        [BindProperty(BinderType = typeof(TrimmingModelBinder))]
        public string Name { get; set; }
        
        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Parent genre")]
        public string SelectedParentGenre { get; set; }

        public SelectList Genres { get; set; }
    }
}