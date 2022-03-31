using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineGameStore.MVC.Models
{
    public class EditGameViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Key { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<SelectListItem> Genres { get; set; }

        public IEnumerable<SelectListItem> PlatformTypes { get; set; }

        public List<int> SelectedGenres { get; set; }

        public List<int> SelectedPlatformTypes { get; set; }
    }
}
