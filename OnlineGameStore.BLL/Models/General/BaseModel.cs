using System;
using OnlineGameStore.BLL.Entities;

namespace OnlineGameStore.BLL.Models.General
{
    public abstract class BaseModel : ISoftDelete
    {
        public string Id { get; set; }
        
        public bool IsDeleted { get; set; }
        
        public DateTime? DeletedAt { get; set; }
    }
}