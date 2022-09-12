using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineGameStore.MVC.Models
{
    public class EditOrderDetailViewModel
    {
        public string GameKey { get; set; }

        [Display(Name = "Product")]
        [ReadOnly(true)]
        public string ProductName { get; set; }

        [DataType(DataType.Currency)]
        [ReadOnly(true)]
        public decimal Price { get; set; }

        [Required]
        public short Quantity { get; set; }

        [Required]
        public float Discount { get; set; }
    }
}