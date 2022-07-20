using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineGameStore.BLL.Enums;
using OnlineGameStore.MVC.Infrastructure;

namespace OnlineGameStore.MVC.Models
{
    public class EditGameViewModel
    {
        [UIHint("HiddenInput")]
        public int Id { get; set; }

        [Required]
        [BindProperty(BinderType = typeof(TrimmingModelBinder))]
        public string Key { get; set; }

        [Required]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Range(0.01, 99_999_999.99)]
        public decimal Price { get; set; } = 0.01M;

        [Required]
        [Range(0, short.MaxValue)]
        [Display(Name = "Units in Stock")]
        public short UnitsInStock { get; set; } = 1;

        [Required]
        [Display(Name = "Is game discontinued?")]
        public bool Discontinued { get; set; }
        
        [UIHint("HiddenInput")]
        public DateTime? DateAdded { get; set; }

        [Required]
        [Display(Name = "Published at")]
        [DataType(DataType.Date)]
        public DateTime? DatePublished { get; set; }
        
        [UIHint("HiddenInput")]
        public ulong ViewsNumber { get; set; }

        [Required]
        [Display(Name = "Quantity per Unit")]
        public string QuantityPerUnit { get; set; }
        
        [UIHint("HiddenInput")]
        public int UnitsOnOrder { get; set; }
        
        [UIHint("HiddenInput")]
        public int ReorderLevel { get; set; }
        
        [UIHint("HiddenInput")]
        public DatabaseEntity DatabaseEntity { get; set; }

        public SelectList Genres { get; set; }

        public SelectList PlatformTypes { get; set; }

        public SelectList Publishers { get; set; }

        [Display(Name = "Choose genres")]
        public List<int> SelectedGenres { get; set; }

        [Display(Name = "Choose platform types")]
        public List<int> SelectedPlatformTypes { get; set; }

        [Display(Name = "Publisher")]
        public string SelectedPublisher { get; set; }
    }
}