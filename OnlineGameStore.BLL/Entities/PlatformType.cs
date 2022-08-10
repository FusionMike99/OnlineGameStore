using System.Collections.Generic;

namespace OnlineGameStore.BLL.Entities
{
    public class PlatformType : BaseEntity
    {
        public string Type { get; set; }

        public ICollection<GamePlatformType> GamePlatformTypes { get; set; }
    }
}