using System.Collections.Generic;
using OnlineGameStore.BLL.Entities;

namespace OnlineGameStore.BLL.Services.Contracts
{
    public interface ICommentService
    {
        Comment LeaveCommentToGame(string gameKey, Comment comment);

        Comment GetCommentById(string commentId);
        
        IEnumerable<Comment> GetAllCommentsByGameKey(string gameKey);
        
        Comment EditComment(Comment comment);

        void DeleteComment(string commentId);
    }
}