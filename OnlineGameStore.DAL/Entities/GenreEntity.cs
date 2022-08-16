using System;
using System.Collections.Generic;

namespace OnlineGameStore.DAL.Entities
{
    public class GenreEntity : BaseEntity
    {
        public string Name { get; set; }
        
        public string Description { get; set; }

        public Guid? ParentId { get; set; }

        public GenreEntity Parent { get; set; }

        public ICollection<GenreEntity> SubGenres { get; set; }

        public ICollection<GameGenreEntity> GameGenres { get; set; }
    }
}