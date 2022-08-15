using System;

namespace OnlineGameStore.BLL.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        
        public bool IsDeleted { get; set; }
        
        public DateTime? DeletedAt { get; set; }
    }
}