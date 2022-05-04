using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineGameStore.MVC.Models
{
    public class GameViewModel
    {
        [UIHint("HiddenInput")]
        public int Id { get; set; }

        public string Key { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Display(Name = "Units in Stock")]
        public short UnitsInStock { get; set; }

        public bool Discontinued { get; set; }

        [Display(Name = "Publisher")]
        public string Publisher { get; set; }

        public IEnumerable<string> Genres { get; set; }

        [Display(Name = "Platform types")]
        public IEnumerable<string> PlatformTypes { get; set; }
    }
}
