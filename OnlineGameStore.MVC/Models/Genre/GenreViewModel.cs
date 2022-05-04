using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineGameStore.MVC.Models
{
    public class GenreViewModel
    {
        [UIHint("HiddenInput")]
        public int Id { get; set; }

        public string Name { get; set; }

        public int? ParentId { get; set; }

        [Display(Name = "Parent genre")]
        public string ParentName { get; set; }

        public List<GenreViewModel> SubGenres { get; set; }
    }
}
