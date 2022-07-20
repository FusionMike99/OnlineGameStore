using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OnlineGameStore.BLL.Enums;

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
        
        [Display(Name = "Quantity per Unit")]
        public string QuantityPerUnit { get; set; }

        public bool Discontinued { get; set; }
        
        [Display(Name = "Added at")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime? DateAdded { get; set; }
        
        [Display(Name = "Published at")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime? DatePublished { get; set; }
        
        [Display(Name = "Views")]
        public ulong ViewsNumber { get; set; }

        [Display(Name = "Publisher")]
        public string Publisher { get; set; }

        public IEnumerable<string> Genres { get; set; }

        [Display(Name = "Platform types")]
        public IEnumerable<string> PlatformTypes { get; set; }
        
        [UIHint("HiddenInput")]
        public DatabaseEntity DatabaseEntity { get; set; }
    }
}