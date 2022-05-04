using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using OnlineGameStore.MVC.Infrastructure;

namespace OnlineGameStore.MVC.Models
{
    public class EditGenreViewModel
    {
        [UIHint("HiddenInput")]
        public int Id { get; set; }

        [Required]
        [BindProperty(BinderType = typeof(TrimmingModelBinder))]
        public string Name { get; set; }

        [Display(Name = "Parent genre")]
        public int? SelectedParentGenre { get; set; }

        public SelectList Genres { get; set; }
    }
}
