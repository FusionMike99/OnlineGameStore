using System.ComponentModel.DataAnnotations;

namespace OnlineGameStore.MVC.Models
{
    public class PriceRangeViewModel
    {
        [Display(Name = "From")]
        [DataType(DataType.Currency)]
        [Range(0.01, 99_999_999.99)]
        public decimal? Min { get; set; }
    
        [Display(Name = "To")]
        [DataType(DataType.Currency)]
        [Range(0.01, 99_999_999.99)]
        public decimal? Max { get; set; }
    }
}