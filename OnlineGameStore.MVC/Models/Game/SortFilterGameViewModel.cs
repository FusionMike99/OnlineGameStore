using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineGameStore.DomainModels.Enums;

namespace OnlineGameStore.MVC.Models
{
    public class SortFilterGameViewModel
    {
        public SelectList Genres { get; set; }
        
        public SelectList PlatformTypes { get; set; }
        
        public SelectList Publishers { get; set; }
        
        public SelectList SortParameters { get; set; }
        
        public SelectList DatePublishedParameters { get; set; }
        
        public List<string> SelectedGenres { get; set; }
        
        public List<string> SelectedPlatformTypes { get; set; }
        
        public List<string> SelectedPublishers { get; set; }
        
        public GameSortState GameSortState { get; set; }
        
        public PriceRangeViewModel PriceRange { get; set; }
        
        public DatePublishedPeriod DatePublishedPeriod { get; set; }
        
        [Display(Name = "Name")]
        [MinLength(3)]
        public string GameName { get; set; }
    }
}