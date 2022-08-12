using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Models;

namespace OnlineGameStore.BLL.Repositories.GameStore
{
    public interface IGameStoreGameRepository : IGameStoreGenericRepository<GameEntity>
    {
        Task<GameEntity> GetByKeyAsync(string gameKey, bool includeDeleted = false, params string[] includeProperties);

        Task<IEnumerable<GameEntity>> GetAllByFilterAsync(SortFilterGameModel sortFilterModel);
    }
}