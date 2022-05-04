using OnlineGameStore.BLL.Entities;
using System.Collections.Generic;

namespace OnlineGameStore.BLL.Services.Contracts
{
    public interface IPlatformTypeService
    {
        PlatformType CreatePlatformType(PlatformType platformType);

        PlatformType EditPlatformType(PlatformType platformType);

        void DeletePlatformType(int platformTypeId);

        PlatformType GetPlatformTypeById(int platformTypeId);

        IEnumerable<PlatformType> GetAllPlatformTypes();

        bool CheckTypeForUniqueness(int platformTypeId, string type);
    }
}
