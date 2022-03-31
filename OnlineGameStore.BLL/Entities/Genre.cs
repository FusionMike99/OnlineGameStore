using System.Collections.Generic;

namespace OnlineGameStore.BLL.Entities
{
    public class Genre : IBaseEntity<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? ParentId { get; set; }

        public Genre Parent { get; set; }

        public ICollection<Genre> SubGenres { get; set; }

        public ICollection<GameGenre> GameGenres { get; set; }
    }
}
