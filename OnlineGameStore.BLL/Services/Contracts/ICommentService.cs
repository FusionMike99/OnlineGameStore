using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Models.General;

namespace OnlineGameStore.BLL.Services.Contracts
{
    public interface ICommentService
    {
        Task<CommentModel> LeaveCommentToGameAsync(string gameKey, CommentModel comment);

        Task<CommentModel> GetCommentByIdAsync(Guid commentId);
        
        Task<IEnumerable<CommentModel>> GetAllCommentsByGameKeyAsync(string gameKey);
        
        Task<CommentModel> EditCommentAsync(CommentModel comment);

        Task DeleteCommentAsync(Guid commentId);
    }
}