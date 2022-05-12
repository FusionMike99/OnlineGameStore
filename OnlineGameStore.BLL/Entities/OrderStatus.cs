using System;
using System.Collections.Generic;

namespace OnlineGameStore.BLL.Entities
{
    public class OrderStatus : IBaseEntity<int>
    {
        public int Id { get; set; }
        
        public string Status { get; set; }

        public ICollection<Order> Orders { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}