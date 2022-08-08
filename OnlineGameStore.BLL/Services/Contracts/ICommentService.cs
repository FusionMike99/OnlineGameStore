using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Models.General;

namespace OnlineGameStore.BLL.Services.Contracts
{
    public interface ICommentService
    {
        Task<CommentModel> LeaveCommentToGame(string gameKey, CommentModel comment);

        Task<CommentModel> GetCommentById(string commentId);
        
        Task<IEnumerable<CommentModel>> GetAllCommentsByGameKey(string gameKey);
        
        Task<CommentModel> EditComment(CommentModel comment);

        Task DeleteComment(string commentId);
    }
}