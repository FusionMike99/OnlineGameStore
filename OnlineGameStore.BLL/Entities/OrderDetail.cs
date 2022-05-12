namespace OnlineGameStore.BLL.Entities
{
    public class OrderDetail
    {
        public int ProductId { get; set; }

        public Game Product { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; }

        public decimal Price { get; set; }

        public short Quantity { get; set; }

        public float Discount { get; set; } = 0F;
    }
}