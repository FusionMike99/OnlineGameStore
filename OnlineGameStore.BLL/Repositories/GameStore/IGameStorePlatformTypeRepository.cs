using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Entities;

namespace OnlineGameStore.BLL.Repositories.GameStore
{
    public interface IGameStorePlatformTypeRepository : IGameStoreGenericRepository<PlatformTypeEntity>
    {
        Task<IEnumerable<string>> GetIdsByTypes(IEnumerable<string> types);

        Task<PlatformTypeEntity> GetByType(string type,
            bool includeDeleted = false,
            params string[] includeProperties);
    }
}