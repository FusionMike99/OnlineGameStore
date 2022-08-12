using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Entities;

namespace OnlineGameStore.BLL.Repositories.GameStore
{
    public interface IGameStoreCommentRepository : IGameStoreGenericRepository<CommentEntity>
    {
        Task<IEnumerable<CommentEntity>> GetAllByGameKey(string gameKey,
            bool includeDeleted = false,
            params string[] includeProperties);
    }
}