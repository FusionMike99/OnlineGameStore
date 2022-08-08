using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Entities;

namespace OnlineGameStore.BLL.Repositories.GameStore
{
    public interface IPlatformTypeRepository : IGenericRepository<PlatformType>
    {
        Task<IEnumerable<string>> GetIdsByTypes(IEnumerable<string> types);

        Task<PlatformType> GetByType(string type,
            bool includeDeleted = false,
            params string[] includeProperties);
    }
}