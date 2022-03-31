namespace OnlineGameStore.BLL.Entities
{
    public class GamePlatformType
    {
        public int GameId { get; set; }

        public Game Game { get; set; }

        public int PlatformId { get; set; }

        public PlatformType PlatformType { get; set; }
    }
}
