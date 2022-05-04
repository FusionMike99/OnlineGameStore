using System;
using System.Collections.Generic;

namespace OnlineGameStore.BLL.Entities
{
    public class Order : IBaseEntity<int>
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public int CustomerId { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedAt { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
