namespace OnlineGameStore.BLL.Models.General
{
    public class GamePlatformTypeModel : BaseModel
    {
        public string GameId { get; set; }

        public GameModel Game { get; set; }

        public string PlatformId { get; set; }

        public PlatformTypeModel PlatformType { get; set; }
    }
}