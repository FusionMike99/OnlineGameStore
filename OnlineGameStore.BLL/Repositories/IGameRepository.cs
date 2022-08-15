﻿using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Models;
using OnlineGameStore.BLL.Models.General;

namespace OnlineGameStore.BLL.Repositories
{
    public interface IGameRepository
    {
        Task CreateAsync(GameModel gameModel);

        Task UpdateOrCreateAsync(GameModel gameModel);

        Task IncreaseGameQuantityAsync(string gameKey, short quantity);
        
        Task DecreaseGameQuantityAsync(string gameKey, short quantity);

        Task DeleteOrCreateAsync(GameModel gameModel);
        
        Task<GameModel> GetByKeyAsync(string gameKey, bool increaseViews = false, bool includeDeleted = false);
        
        Task<(IEnumerable<GameModel>, int)> GetAllAsync(SortFilterGameModel sortFilterModel = null,
            PageModel pageModel = null);

        Task<int> GetGamesNumberAsync(SortFilterGameModel sortFilterModel = null);
    }
}