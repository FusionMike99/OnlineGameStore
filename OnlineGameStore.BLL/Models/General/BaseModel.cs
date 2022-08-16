using System;

namespace OnlineGameStore.BLL.Models.General
{
    public abstract class BaseModel
    {
        public Guid Id { get; set; }
        
        public bool IsDeleted { get; set; }
        
        public DateTime? DeletedAt { get; set; }
    }
}