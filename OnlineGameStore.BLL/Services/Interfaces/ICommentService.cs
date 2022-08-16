using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.DomainModels.Models.General;

namespace OnlineGameStore.BLL.Services.Interfaces
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