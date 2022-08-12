using System;

namespace OnlineGameStore.BLL.Entities
{
    public class GameGenreEntity : BaseEntity
    {
        public Guid GameId { get; set; }

        public GameEntity Game { get; set; }

        public Guid GenreId { get; set; }

        public GenreEntity Genre { get; set; }
    }
}