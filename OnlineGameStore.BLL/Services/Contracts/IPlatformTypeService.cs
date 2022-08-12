using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Models.General;

namespace OnlineGameStore.BLL.Services.Contracts
{
    public interface IPlatformTypeService
    {
        Task<PlatformTypeModel> CreatePlatformTypeAsync(PlatformTypeModel platformType);

        Task<PlatformTypeModel> EditPlatformTypeAsync(PlatformTypeModel platformType);

        Task DeletePlatformTypeAsync(Guid platformTypeId);

        Task<PlatformTypeModel> GetPlatformTypeByIdAsync(Guid platformTypeId);

        Task<IEnumerable<PlatformTypeModel>> GetAllPlatformTypesAsync();
        
        Task<IEnumerable<string>> GetPlatformTypesIdsByNamesAsync(IEnumerable<string> types);

        Task<bool> CheckTypeForUniqueAsync(Guid platformTypeId, string type);
    }
}