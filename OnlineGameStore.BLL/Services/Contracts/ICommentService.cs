using System.Collections.Generic;
using OnlineGameStore.BLL.Entities;

namespace OnlineGameStore.BLL.Services.Contracts
{
    public interface ICommentService
    {
        Comment LeaveCommentToGame(string gameKey, Comment comment);

        IEnumerable<Comment> GetAllCommentsByGameKey(string gameKey);
    }
}