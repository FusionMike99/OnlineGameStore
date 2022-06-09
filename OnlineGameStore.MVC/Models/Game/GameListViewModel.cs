using System.Collections.Generic;

namespace OnlineGameStore.MVC.Models
{
    public class GameListViewModel
    {
        public IEnumerable<GameViewModel> Games { get; set; }
        
        public SortFilterGameViewModel SortFilterGameViewModel { get; set; }
        
        public PageViewModel PageViewModel { get; set; }
    }
}