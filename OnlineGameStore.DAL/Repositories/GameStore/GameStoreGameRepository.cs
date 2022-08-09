using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Models;
using OnlineGameStore.BLL.Pipelines;
using OnlineGameStore.BLL.Pipelines.Filters.Games;
using OnlineGameStore.BLL.Repositories.GameStore;
using OnlineGameStore.DAL.Data;

namespace OnlineGameStore.DAL.Repositories.GameStore
{
    public class GameStoreGameRepository : GameStoreGenericRepository<Game>, IGameStoreGameRepository
    {
        public GameStoreGameRepository(StoreDbContext context) : base(context)
        {
        }

        public async Task<Game> GetByKey(string gameKey,
            bool includeDeleted = false,
            params string[] includeProperties)
        {
            Expression<Func<Game, bool>> predicate = g => g.Key == gameKey;

            return await GetSingle(predicate, includeDeleted, includeProperties);
        }

        public async Task<IEnumerable<Game>> GetAllByFilter(SortFilterGameModel sortFilterModel)
        {
            var predicate = GetGameStorePredicate(sortFilterModel);

            return await GetMany(predicate, includeDeleted: true);
        }
        
        private static Expression<Func<Game, bool>> GetGameStorePredicate(SortFilterGameModel model)
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