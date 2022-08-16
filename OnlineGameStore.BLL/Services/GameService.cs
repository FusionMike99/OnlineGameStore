using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OnlineGameStore.BLL.Services.Interfaces;
using OnlineGameStore.DAL.Abstractions.Interfaces;
using OnlineGameStore.DomainModels.Models;
using OnlineGameStore.DomainModels.Models.General;

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
                _logger.LogError("Deleting game with game key {GameKey} unsuccessfully", gameKey);
                throw new InvalidOperationException("Game has not been found");
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
            var game = await _gameRepository.GetByKeyIncludeDeletedAsync(gameKey);

            return game != null && game.Id != gameId;
        }
    }
}