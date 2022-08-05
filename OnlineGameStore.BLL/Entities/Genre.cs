using System;
using System.Collections.Generic;

namespace OnlineGameStore.BLL.Entities
{
    public class Genre : IBaseEntity<Guid>
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }

        public Guid? ParentId { get; set; }

        public Genre Parent { get; set; }

        public ICollection<Genre> SubGenres { get; set; }

        public ICollection<GameGenre> GameGenres { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}