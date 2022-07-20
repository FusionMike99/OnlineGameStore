using System;

namespace OnlineGameStore.BLL.Models
{
    public class FilterOrderModel
    {
        public DateTime? MinDate { get; set; }
        
        public DateTime? MaxDate { get; set; }
    }
}