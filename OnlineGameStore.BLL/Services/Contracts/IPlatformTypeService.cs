using System.Collections.Generic;
using OnlineGameStore.BLL.Entities;

namespace OnlineGameStore.BLL.Services.Contracts
{
    public interface IPlatformTypeService
    {
        PlatformType CreatePlatformType(PlatformType platformType);

        PlatformType EditPlatformType(PlatformType platformType);

        void DeletePlatformType(int platformTypeId);

        PlatformType GetPlatformTypeById(int platformTypeId);

        IEnumerable<PlatformType> GetAllPlatformTypes();

        bool CheckTypeForUnique(int platformTypeId, string type);
    }
}