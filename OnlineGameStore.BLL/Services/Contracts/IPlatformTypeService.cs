using System.Collections.Generic;
using OnlineGameStore.BLL.Entities;

namespace OnlineGameStore.BLL.Services.Contracts
{
    public interface IPlatformTypeService
    {
        PlatformType CreatePlatformType(PlatformType platformType);

        PlatformType EditPlatformType(PlatformType platformType);

        void DeletePlatformType(string platformTypeId);

        PlatformType GetPlatformTypeById(string platformTypeId);

        IEnumerable<PlatformType> GetAllPlatformTypes();
        
        IEnumerable<string> GetPlatformTypesIdsByNames(IEnumerable<string> types);

        bool CheckTypeForUnique(string platformTypeId, string type);
    }
}