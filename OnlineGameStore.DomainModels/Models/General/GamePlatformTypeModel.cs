using System;

namespace OnlineGameStore.DomainModels.Models.General
{
    public class GamePlatformTypeModel : BaseModel
    {
        public Guid GameId { get; set; }

        public GameModel Game { get; set; }

        public Guid PlatformId { get; set; }

        public PlatformTypeModel PlatformType { get; set; }
    }
}