﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.DomainModels.Models;
using OnlineGameStore.DomainModels.Models.General;

namespace OnlineGameStore.BLL.Services.Interfaces
{
    public interface IGameService
    {
        Task<GameModel> CreateGameAsync(GameModel game);

        Task<GameModel> EditGameAsync(GameModel game);

        Task DeleteGameAsync(string gameKey);

        Task<GameModel> GetGameByKeyAsync(string gameKey, bool increaseViews = false);
        
        Task<(IEnumerable<GameModel>, int)> GetAllGamesAsync(SortFilterGameModel sortFilterModel = null,
            PageModel pageModel = null);

        Task<int> GetGamesNumberAsync(SortFilterGameModel sortFilterModel = null);

        Task<bool> CheckKeyForUniqueAsync(Guid gameId, string gameKey);
    }
}