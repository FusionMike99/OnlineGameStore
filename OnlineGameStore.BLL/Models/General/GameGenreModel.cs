namespace OnlineGameStore.BLL.Models.General
{
    public class GameGenreModel : BaseModel
    {
        public string GameId { get; set; }

        public GameModel Game { get; set; }

        public string GenreId { get; set; }

        public GenreModel Genre { get; set; }
    }
}