using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineGameStore.MVC.Models
{
    public class GenreViewModel
    {
        [UIHint("HiddenInput")]
        public string Id { get; set; }

        public string Name { get; set; }
        
        public string Description { get; set; }

        public string ParentId { get; set; }

        [Display(Name = "Parent genre")]
        public string ParentName { get; set; }

        public List<GenreViewModel> SubGenres { get; set; }
    }
}