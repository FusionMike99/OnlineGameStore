using System;
using System.Collections.Generic;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Models;

namespace OnlineGameStore.BLL.Services.Contracts
{
    public interface IGameService
    {
        Game CreateGame(Game game);

        Game EditGame(string gameKey, Game game);

        void DeleteGame(string gameKey);

        Game GetGameByKey(string gameKey, bool increaseViews = false);

        void UpdateGameQuantity(string gameKey, short quantity, Func<short, short, short> operation);
        
        IEnumerable<Game> GetAllGames(SortFilterGameModel sortFilterModel = null, PageModel pageModel = null);
        
        IEnumerable<Game> GetAllGames(out int gamesNumber, 
            SortFilterGameModel sortFilterModel = null,
            PageModel pageModel = null);

        int GetGamesNumber(SortFilterGameModel sortFilterModel = null);

        bool CheckKeyForUnique(int gameId, string gameKey);
    }
}