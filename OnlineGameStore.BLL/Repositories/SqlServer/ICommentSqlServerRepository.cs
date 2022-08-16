using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Entities;

namespace OnlineGameStore.BLL.Repositories.SqlServer
{
    public interface ICommentSqlServerRepository : ISqlServerRepository<CommentEntity>
    {
        Task<IEnumerable<CommentEntity>> GetAllByGameKeyAsync(string gameKey, bool includeDeleted = false,
            params string[] includeProperties);
    }
}