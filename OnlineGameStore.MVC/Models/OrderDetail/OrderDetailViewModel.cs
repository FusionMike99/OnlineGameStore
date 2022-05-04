using System.ComponentModel.DataAnnotations;

namespace OnlineGameStore.MVC.Models
{
    public class OrderDetailViewModel
    {
        public int ProductId { get; set; }

        [Display(Name = "Product")]
        public string ProductName { get; set; }

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public short Quantity { get; set; }

        public float Discount { get; set; } = 0F;
    }
}
