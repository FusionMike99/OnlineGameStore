using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OnlineGameStore.BLL.Models;
using OnlineGameStore.BLL.Models.General;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Services.Contracts;

namespace OnlineGameStore.BLL.Services
{
    public class GameService : IGameService
    {
        private readonly ILogger<GameService> _logger;
        private readonly IGameRepository _gameRepository;

        public GameService(ILogger<GameService> logger,
            IGameRepository gameRepository)
        {
            _logger = logger;
            _gameRepository = gameRepository;
        }

        public async Task<GameModel> CreateGame(GameModel game)
        {
            await _gameRepository.CreateAsync(game);

            _logger.LogDebug($@"Class: {nameof(GameService)}; Method: {nameof(CreateGame)}.
                    Creating game with id {game.Id} successfully", game);

            return game;
        }

        public async Task DeleteGame(string gameKey)
        {
            var game = await GetGameByKey(gameKey);

            if (game == null)
            {
                var exception = new InvalidOperationException("Game has not been found");

                _logger.LogError(exception, $@"Class: {nameof(GameService)}; Method: {nameof(DeleteGame)}.
                    Deleting game with id {gameKey} unsuccessfully", gameKey);

                throw exception;
            }

            await _gameRepository.DeleteAsync(game);

            _logger.LogDebug($@"Class: {nameof(GameService)}; Method: {nameof(DeleteGame)}.
                    Deleting game with id {gameKey} successfully", game);
        }

        public async Task<GameModel> EditGame(GameModel game)
        {
            await _gameRepository.UpdateAsync(game);

            _logger.LogDebug($@"Class: {nameof(GameService)}; Method: {nameof(EditGame)}.
                    Editing game with id {game.Id} successfully", game);

            return game;
        }

        public async Task<(IEnumerable<GameModel>, int)> GetAllGames(SortFilterGameModel sortFilterModel = null,
            PageModel pageModel = null)
        {
            var result = await _gameRepository.GetAllAsync(sortFilterModel, pageModel);

            return result;
        }

        public async Task<GameModel> GetGameByKey(string gameKey, bool increaseViews = false)
        {
            var game = await _gameRepository.GetByKeyAsync(gameKey, increaseViews);

            return game;
        }

        public async Task<int> GetGamesNumber(SortFilterGameModel sortFilterModel = null)
        {
            var gamesNumber = await _gameRepository.GetGamesNumberAsync(sortFilterModel);

            return gamesNumber;
        }

        public async Task<bool> CheckKeyForUnique(Guid gameId, string gameKey)
        {
            var game = await _gameRepository.GetByKeyAsync(gameKey, increaseViews: false, includeDeleted: true);

            return game != null && game.Id != gameId;
        }
    }
}