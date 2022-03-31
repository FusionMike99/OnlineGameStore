using OnlineGameStore.BLL.Entities;
using System.Collections.Generic;

namespace OnlineGameStore.BLL.Services.Contracts
{
    public interface ICommentService
    {
        Comment LeaveCommentToGame(string gameKey, Comment comment);

        IEnumerable<Comment> GetAllCommentsByGameKey(string gameKey);
    }
}
