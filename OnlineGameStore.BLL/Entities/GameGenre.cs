using System;

namespace OnlineGameStore.BLL.Entities
{
    public class GameGenre : BaseEntity
    {
        public Guid GameId { get; set; }

        public Game Game { get; set; }

        public Guid GenreId { get; set; }

        public Genre Genre { get; set; }
    }
}