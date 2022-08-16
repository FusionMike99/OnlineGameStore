using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.DomainModels.Models.General;

namespace OnlineGameStore.BLL.Services.Interfaces
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