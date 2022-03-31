using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineGameStore.BLL.Services
{
    public class GameService : IGameService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GameService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Game CreateGame(Game game)
        {
            game = _unitOfWork.Games.Create(game);
            _unitOfWork.Commit();

            return game;
        }

        public void DeleteGame(int gameId)
        {
            var game = _unitOfWork.Games
                .GetSingle(g => g.Id == gameId);

            if (game == null)
            {
                throw new InvalidOperationException("Game has not been found");
            }

            _unitOfWork.Games.Delete(game);
            _unitOfWork.Commit();
        }

        public Game EditGame(Game game)
        {
            game = _unitOfWork.Games.Update(game);
            _unitOfWork.Commit();

            return game;
        }

        public IEnumerable<Game> GetAllGames()
        {
            var games = _unitOfWork.Games
                .GetMany(
                    null,
                    $"{nameof(Game.GameGenres)}.{nameof(GameGenre.Genre)}",
                    $"{nameof(Game.GamePlatformTypes)}.{nameof(GamePlatformType.PlatformType)}");

            return games;
        }

        public Game GetGameByKey(string gameKey)
        {
            var game = _unitOfWork.Games
                .GetSingle(
                    g => g.Key == gameKey,
                    $"{nameof(Game.GameGenres)}.{nameof(GameGenre.Genre)}",
                    $"{nameof(Game.GamePlatformTypes)}.{nameof(GamePlatformType.PlatformType)}");

            return game;
        }

        public IEnumerable<Game> GetGamesByGenre(int genreId)
        {
            var games = _unitOfWork.Games
                .GetMany(
                    g => g.GameGenres.Any(genre => genre.GenreId == genreId),
                    $"{nameof(Game.GameGenres)}.{nameof(GameGenre.Genre)}",
                    $"{nameof(Game.GamePlatformTypes)}.{nameof(GamePlatformType.PlatformType)}");

            return games;
        }

        public IEnumerable<Game> GetGamesByPlatformType(int typeId)
        {
            var games = _unitOfWork.Games
                .GetMany(
                    g => g.GamePlatformTypes.Any(platformType => platformType.PlatformId == typeId),
                    $"{nameof(Game.GameGenres)}.{nameof(GameGenre.Genre)}",
                    $"{nameof(Game.GamePlatformTypes)}.{nameof(GamePlatformType.PlatformType)}");

            return games;
        }
    }
}
