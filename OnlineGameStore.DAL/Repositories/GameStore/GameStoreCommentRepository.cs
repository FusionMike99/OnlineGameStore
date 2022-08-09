using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Repositories.GameStore;
using OnlineGameStore.DAL.Data;

namespace OnlineGameStore.DAL.Repositories.GameStore
{
    public class GameStoreCommentRepository : GameStoreGenericRepository<Comment>, IGameStoreCommentRepository
    {
        public GameStoreCommentRepository(StoreDbContext context) : base(context)
        {
        }

        public Task<IEnumerable<Comment>> GetAllByGameKey(string gameKey,
            bool includeDeleted = false,
            params string[] includeProperties)
        {
            Expression<Func<Comment, bool>> predicate = c => c.Game.Key == gameKey;

            return GetMany(predicate: predicate,
                includeDeleted: includeDeleted,
                includeProperties: includeProperties);
        }
    }
}