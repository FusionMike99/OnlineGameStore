using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.DAL.Entities;
using OnlineGameStore.DomainModels.Models;

namespace OnlineGameStore.DAL.Repositories.SqlServer.Interfaces
{
    public interface IGameSqlServerRepository : ISqlServerRepository<GameEntity>
    {
        Task<GameEntity> GetByKeyAsync(string gameKey, bool includeDeleted = false, params string[] includeProperties);

        Task<IEnumerable<GameEntity>> GetAllByFilterAsync(SortFilterGameModel sortFilterModel);
    }
}