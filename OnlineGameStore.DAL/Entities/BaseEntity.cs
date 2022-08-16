using System;

namespace OnlineGameStore.DAL.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        
        public bool IsDeleted { get; set; }
        
        public DateTime? DeletedAt { get; set; }
    }
}