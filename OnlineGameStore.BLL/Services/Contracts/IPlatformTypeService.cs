using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Models.General;

namespace OnlineGameStore.BLL.Services.Contracts
{
    public interface IPlatformTypeService
    {
        Task<PlatformTypeModel> CreatePlatformType(PlatformTypeModel platformType);

        Task<PlatformTypeModel> EditPlatformType(PlatformTypeModel platformType);

        Task DeletePlatformType(Guid platformTypeId);

        Task<PlatformTypeModel> GetPlatformTypeById(Guid platformTypeId);

        Task<IEnumerable<PlatformTypeModel>> GetAllPlatformTypes();
        
        Task<IEnumerable<string>> GetPlatformTypesIdsByNames(IEnumerable<string> types);

        Task<bool> CheckTypeForUnique(Guid platformTypeId, string type);
    }
}