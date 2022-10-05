using System;
using OnlineGameStore.DomainModels.Enums;

namespace OnlineGameStore.DomainModels.Models
{
    public class FilterOrderModel
    {
        public DateTime? MinDate { get; set; }
        
        public DateTime? MaxDate { get; set; }
        
        public DatabaseEntity DatabaseEntity { get; set; }
    }
}