using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Models.General;

namespace OnlineGameStore.BLL.Repositories
{
    public interface IPlatformTypeRepository
    {
        Task CreateAsync(PlatformTypeModel platformTypeModel);

        Task UpdateAsync(PlatformTypeModel platformTypeModel);

        Task DeleteAsync(PlatformTypeModel platformTypeModel);
        
        Task<PlatformTypeModel> GetByIdAsync(string id,
            bool includeDeleted = false,
            params string[] includeProperties);
        
        Task<PlatformTypeModel> GetByTypeAsync(string type,
            bool includeDeleted = false,
            params string[] includeProperties);

        Task<IEnumerable<string>> GetIdsByTypesAsync(IEnumerable<string> types);

        Task<IEnumerable<PlatformTypeModel>> GetAllAsync(bool includeDeleted = false,
            params string[] includeProperties);
    }
}