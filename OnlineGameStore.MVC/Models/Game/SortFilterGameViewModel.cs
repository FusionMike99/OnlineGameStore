using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineGameStore.BLL.Enums;
using OnlineGameStore.BLL.Models;
using OnlineGameStore.MVC.Infrastructure;

namespace OnlineGameStore.MVC.Models
{
    public class SortFilterGameViewModel
    {
        public SelectList Genres { get; set; }
        
        public SelectList PlatformTypes { get; set; }
        
        public SelectList Publishers { get; set; }
        
        public SelectList SortParameters { get; set; }
        
        public SelectList DatePublishedParameters { get; set; }
        
        public List<int> SelectedGenres { get; set; }
        
        public List<int> SelectedPlatformTypes { get; set; }
        
        public List<int> SelectedPublishers { get; set; }
        
        public GameSortState GameSortState { get; set; }
        
        public PriceRangeViewModel PriceRange { get; set; }
        
        public DatePublishedPeriod DatePublishedPeriod { get; set; }
        
        [Display(Name = "Name")]
        [MinLength(3)]
        public string GameName { get; set; }
    }
}