﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineGameStore.DAL.Data;
using OnlineGameStore.DAL.Entities;
using OnlineGameStore.DAL.Repositories.SqlServer.Extensions;
using OnlineGameStore.DAL.Repositories.SqlServer.Interfaces;

namespace OnlineGameStore.DAL.Repositories.SqlServer
{
    public class CommentSqlServerRepository : SqlServerRepository<CommentEntity>, ICommentSqlServerRepository
    {
        public CommentSqlServerRepository(StoreDbContext context, ILoggerFactory logger) : base(context, logger)
        {
        }

        public async Task<IEnumerable<CommentEntity>> GetAllByGameKeyAsync(string gameKey)
        {
            var comments = await Entities.IncludeDeleted().Where(c => c.Game.Key == gameKey)
                .Include(c => c.Replies).ToListAsync();

            return comments;
        }
    }
}