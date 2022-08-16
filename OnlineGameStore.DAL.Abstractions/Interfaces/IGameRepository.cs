using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.DomainModels.Models;
using OnlineGameStore.DomainModels.Models.General;

namespace OnlineGameStore.DAL.Abstractions.Interfaces
{
    public interface IGameRepository
    {
        Task CreateAsync(GameModel gameModel);

        Task UpdateAsync(GameModel gameModel);

        Task IncreaseGameQuantityAsync(string gameKey, short quantity);
        
        Task DecreaseGameQuantityAsync(string gameKey, short quantity);

        Task DeleteAsync(GameModel gameModel);
        
        Task<GameModel> GetByKeyAsync(string gameKey, bool increaseViews = false);
        
        Task<GameModel> GetByKeyIncludeDeletedAsync(string gameKey);
        
        Task<(IEnumerable<GameModel>, int)> GetAllAsync(SortFilterGameModel sortFilterModel = null,
            PageModel pageModel = null);

        Task<int> GetGamesNumberAsync(SortFilterGameModel sortFilterModel = null);
    }
}