using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.DAL.Entities;

namespace OnlineGameStore.DAL.Repositories.SqlServer.Interfaces
{
    public interface ICommentSqlServerRepository : ISqlServerRepository<CommentEntity>
    {
        Task<IEnumerable<CommentEntity>> GetAllByGameKeyAsync(string gameKey, bool includeDeleted = false,
            params string[] includeProperties);
    }
}