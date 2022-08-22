using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineGameStore.MVC.Models
{
    public class FilterOrderViewModel
    {
        [Display(Name = "From")]
        [DataType(DataType.Date)]
        public DateTime? MinDate { get; set; }
        
        [Display(Name = "To")]
        [DataType(DataType.Date)]
        public DateTime? MaxDate { get; set; }
    }
}