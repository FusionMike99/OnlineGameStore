using System;
using System.Collections.Generic;

namespace OnlineGameStore.DomainModels.Models.General
{
    public class GenreModel : BaseModel
    {
        public string Name { get; set; }
        
        public string Description { get; set; }

        public Guid? ParentId { get; set; }

        public GenreModel Parent { get; set; }

        public ICollection<GenreModel> SubGenres { get; set; }

        public ICollection<GameGenreModel> GameGenres { get; set; }
    }
}