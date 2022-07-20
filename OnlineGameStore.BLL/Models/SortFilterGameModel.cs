using System.Collections.Generic;
using OnlineGameStore.BLL.Enums;

namespace OnlineGameStore.BLL.Models
{
    public class SortFilterGameModel
    {
        public List<int> SelectedGenres { get; set; }
        
        public List<int> SelectedPlatformTypes { get; set; }
        
        public List<string> SelectedPublishers { get; set; }
        
        public List<int> SelectedCategories { get; set; }
        
        public List<int> SelectedSuppliers { get; set; }
        
        public GameSortState GameSortState { get; set; }
        
        public PriceRangeModel PriceRange { get; set; }
        
        public DatePublishedPeriod DatePublishedPeriod { get; set; }
        
        public string GameName { get; set; }
    }
}