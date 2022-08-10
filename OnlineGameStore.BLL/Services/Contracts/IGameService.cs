using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Models;
using OnlineGameStore.BLL.Models.General;

namespace OnlineGameStore.BLL.Services.Contracts
{
    public interface IGameService
    {
        Task<GameModel> CreateGame(GameModel game);

        Task<GameModel> EditGame(GameModel game);

        Task DeleteGame(string gameKey);

        Task<GameModel> GetGameByKey(string gameKey, bool increaseViews = false);
        
        Task<(IEnumerable<GameModel>, int)> GetAllGames(SortFilterGameModel sortFilterModel = null,
            PageModel pageModel = null);

        Task<int> GetGamesNumber(SortFilterGameModel sortFilterModel = null);

        Task<bool> CheckKeyForUnique(Guid gameId, string gameKey);
    }
}