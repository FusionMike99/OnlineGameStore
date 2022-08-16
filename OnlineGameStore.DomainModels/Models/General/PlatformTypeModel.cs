using System.Collections.Generic;

namespace OnlineGameStore.DomainModels.Models.General
{
    public class PlatformTypeModel : BaseModel
    {
        public string Type { get; set; }

        public ICollection<GamePlatformTypeModel> GamePlatformTypes { get; set; }
    }
}