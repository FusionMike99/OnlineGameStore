using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Models;
using OnlineGameStore.BLL.Pipelines;
using OnlineGameStore.BLL.Pipelines.Filters.Games;
using OnlineGameStore.BLL.Repositories.GameStore;
using OnlineGameStore.DAL.Data;

namespace OnlineGameStore.DAL.Repositories.GameStore
{
    public class GameStoreGameRepository : GameStoreGenericRepository<GameEntity>, IGameStoreGameRepository
    {
        public GameStoreGameRepository(StoreDbContext context, ILoggerFactory logger) : base(context, logger)
        {
        }

        public async Task<GameEntity> GetByKeyAsync(string gameKey,
            bool includeDeleted = false,
            params string[] includeProperties)
        {
            Expression<Func<GameEntity, bool>> predicate = g => g.Key == gameKey;

            return await GetSingle(predicate, includeDeleted, includeProperties);
        }

        public async Task<IEnumerable<GameEntity>> GetAllByFilterAsync(SortFilterGameModel sortFilterModel)
        {
            var predicate = GetGameStorePredicate(sortFilterModel);

            return await GetMany(predicate, includeDeleted: true);
        }
        
        private static Expression<Func<GameEntity, bool>> GetGameStorePredicate(SortFilterGameModel model)
        {
            var gamesFilterPipeline = new GamesFilterPipeline()
                .Register(new GamesByGenresFilter())
                .Register(new GamesByPlatformTypesFilter())
                .Register(new GamesByPublishersFilter())
                .Register(new GamesByPriceRangeFilter())
                .Register(new GamesByDatePublishedFilter())
                .Register(new GamesByNameFilter());

            var predicate = gamesFilterPipeline.Process(model);

            return predicate;
        }
    }
}