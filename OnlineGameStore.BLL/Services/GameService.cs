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

        public async Task<GameModel> CreateGameAsync(GameModel game)
        {
            await _gameRepository.CreateAsync(game);

            return game;
        }

        public async Task DeleteGameAsync(string gameKey)
        {
            var game = await GetGameByKeyAsync(gameKey);

            if (game == null)
            {
                var exception = new InvalidOperationException("Game has not been found");

                _logger.LogError(exception, @"Service: {Service}; Method: {Method}.
                    Deleting game with id {GameKey} unsuccessfully", nameof(GameService), nameof(DeleteGameAsync), gameKey);

                throw exception;
            }

            await _gameRepository.DeleteAsync(game);
        }

        public async Task<GameModel> EditGameAsync(GameModel game)
        {
            await _gameRepository.UpdateAsync(game);

            return game;
        }

        public async Task<(IEnumerable<GameModel>, int)> GetAllGamesAsync(SortFilterGameModel sortFilterModel = null,
            PageModel pageModel = null)
        {
            var result = await _gameRepository.GetAllAsync(sortFilterModel, pageModel);

            return result;
        }

        public async Task<GameModel> GetGameByKeyAsync(string gameKey, bool increaseViews = false)
        {
            var game = await _gameRepository.GetByKeyAsync(gameKey, increaseViews);

            return game;
        }

        public async Task<int> GetGamesNumberAsync(SortFilterGameModel sortFilterModel = null)
        {
            var gamesNumber = await _gameRepository.GetGamesNumberAsync(sortFilterModel);

            return gamesNumber;
        }

        public async Task<bool> CheckKeyForUniqueAsync(Guid gameId, string gameKey)
        {
            var game = await _gameRepository.GetByKeyAsync(gameKey, increaseViews: false, includeDeleted: true);

            return game != null && game.Id != gameId;
        }
    }
}