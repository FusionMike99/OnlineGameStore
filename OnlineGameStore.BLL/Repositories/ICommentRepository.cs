using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Models.General;

namespace OnlineGameStore.BLL.Repositories
{
    public interface ICommentRepository
    {
        Task CreateAsync(CommentModel commentModel);

        Task UpdateAsync(CommentModel commentModel);

        Task DeleteAsync(CommentModel commentModel);
        
        Task<CommentModel> GetByIdAsync(Guid id, bool includeDeleted = false, params string[] includeProperties);

        Task<IEnumerable<CommentModel>> GetAllByGameKeyAsync(string gameKey, bool includeDeleted = false,
            params string[] includeProperties);
    }
}