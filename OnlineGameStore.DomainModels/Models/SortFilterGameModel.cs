using System.Collections.Generic;
using OnlineGameStore.DomainModels.Enums;

namespace OnlineGameStore.DomainModels.Models
{
    public class SortFilterGameModel
    {
        public List<string> SelectedGenres { get; set; }
        
        public List<string> SelectedPlatformTypes { get; set; }
        
        public List<string> SelectedPublishers { get; set; }
        
        public List<string> SelectedCategories { get; set; }
        
        public List<string> SelectedSuppliers { get; set; }
        
        public GameSortState GameSortState { get; set; }
        
        public PriceRangeModel PriceRange { get; set; }
        
        public DatePublishedPeriod DatePublishedPeriod { get; set; }
        
        public string GameName { get; set; }
    }
}