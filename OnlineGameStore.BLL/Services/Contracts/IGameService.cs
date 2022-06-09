using System.Collections.Generic;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Models;

namespace OnlineGameStore.BLL.Services.Contracts
{
    public interface IGameService
    {
        Game CreateGame(Game game);

        Game EditGame(Game game);

        void DeleteGame(int gameId);

        Game GetGameByKey(string gameKey, bool increaseViews = false);
        
        IEnumerable<Game> GetAllGames(SortFilterGameModel sortFilterModel = null, PageModel pageModel = null);

        int GetGamesNumber(SortFilterGameModel sortFilterModel = null);

        IEnumerable<Game> GetGamesByGenre(int genreId);

        IEnumerable<Game> GetGamesByPlatformType(int typeId);

        bool CheckKeyForUnique(int gameId, string gameKey);
    }
}