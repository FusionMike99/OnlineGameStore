using System.Collections.Generic;

namespace OnlineGameStore.MVC.Models
{
    public class OrderListViewModel
    {
        public IEnumerable<OrderViewModel> Orders { get; set; }
        
        public FilterOrderViewModel FilterOrderViewModel { get; set; }
    }
}