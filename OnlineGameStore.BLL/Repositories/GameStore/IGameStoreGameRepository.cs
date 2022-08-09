using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Models;

namespace OnlineGameStore.BLL.Repositories.GameStore
{
    public interface IGameStoreGameRepository : IGameStoreGenericRepository<Game>
    {
        Task<Game> GetByKey(string gameKey,
            bool includeDeleted = false,
            params string[] includeProperties);

        Task<IEnumerable<Game>> GetAllByFilter(SortFilterGameModel sortFilterModel);
    }
}