using System;

namespace OnlineGameStore.DomainModels.Models
{
    public class FilterOrderModel
    {
        public DateTime? MinDate { get; set; }
        
        public DateTime? MaxDate { get; set; }
    }
}