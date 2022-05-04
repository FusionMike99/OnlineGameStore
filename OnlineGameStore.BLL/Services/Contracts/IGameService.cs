using OnlineGameStore.BLL.Entities;
using System.Collections.Generic;

namespace OnlineGameStore.BLL.Services.Contracts
{
    public interface IGameService
    {
        Game CreateGame(Game game);

        Game EditGame(Game game);

        void DeleteGame(int gameId);

        Game GetGameByKey(string gameKey);

        IEnumerable<Game> GetAllGames();

        IEnumerable<Game> GetGamesByGenre(int genreId);

        IEnumerable<Game> GetGamesByPlatformType(int typeId);

        bool CheckKeyForUniqueness(int gameId, string gameKey);
    }
}
