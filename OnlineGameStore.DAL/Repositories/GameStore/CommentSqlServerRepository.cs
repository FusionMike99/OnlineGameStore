using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Repositories.SqlServer;
using OnlineGameStore.DAL.Data;

namespace OnlineGameStore.DAL.Repositories.GameStore
{
    public class CommentSqlServerRepository : SqlServerRepository<CommentEntity>, ICommentSqlServerRepository
    {
        public CommentSqlServerRepository(StoreDbContext context, ILoggerFactory logger) : base(context, logger)
        {
        }

        public async Task<IEnumerable<CommentEntity>> GetAllByGameKeyAsync(string gameKey,
            bool includeDeleted = false,
            params string[] includeProperties)
        {
            Expression<Func<CommentEntity, bool>> predicate = c => c.Game.Key == gameKey;
            var comments = await IncludeProperties(includeDeleted, includeProperties)
                .Where(predicate).ToListAsync();

            return comments;
        }
    }
}