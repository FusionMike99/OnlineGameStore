using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Entities;

namespace OnlineGameStore.BLL.Repositories.GameStore
{
    public interface IGameStorePlatformTypeRepository : IGameStoreGenericRepository<PlatformTypeEntity>
    {
        Task<IEnumerable<string>> GetIdsByTypesAsync(IEnumerable<string> types);

        Task<PlatformTypeEntity> GetByTypeAsync(string type, bool includeDeleted = false,
            params string[] includeProperties);
    }
}