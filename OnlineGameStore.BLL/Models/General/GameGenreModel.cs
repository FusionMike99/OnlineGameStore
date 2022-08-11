using System;

namespace OnlineGameStore.BLL.Models.General
{
    public class GameGenreModel : BaseModel
    {
        public Guid GameId { get; set; }

        public GameModel Game { get; set; }

        public Guid GenreId { get; set; }

        public GenreModel Genre { get; set; }
    }
}