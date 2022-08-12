using System.Collections.Generic;

namespace OnlineGameStore.BLL.Entities
{
    public class PlatformTypeEntity : BaseEntity
    {
        public string Type { get; set; }

        public ICollection<GamePlatformTypeEntity> GamePlatformTypes { get; set; }
    }
}