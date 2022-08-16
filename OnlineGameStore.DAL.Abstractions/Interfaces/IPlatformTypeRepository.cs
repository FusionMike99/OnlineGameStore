using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.DomainModels.Models.General;

namespace OnlineGameStore.DAL.Abstractions.Interfaces
{
    public interface IPlatformTypeRepository
    {
        Task CreateAsync(PlatformTypeModel platformTypeModel);

        Task UpdateAsync(PlatformTypeModel platformTypeModel);

        Task DeleteAsync(PlatformTypeModel platformTypeModel);
        
        Task<PlatformTypeModel> GetByIdAsync(Guid id);
        
        Task<PlatformTypeModel> GetByTypeAsync(string type);
        
        Task<PlatformTypeModel> GetByTypeIncludeDeletedAsync(string type);

        Task<IEnumerable<string>> GetIdsByTypesAsync(IEnumerable<string> types);

        Task<IEnumerable<PlatformTypeModel>> GetAllAsync();
    }
}