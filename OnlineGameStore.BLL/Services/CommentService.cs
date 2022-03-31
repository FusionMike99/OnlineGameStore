using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Services.Contracts;
using System.Collections.Generic;

namespace OnlineGameStore.BLL.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGameService _gameService;

        public CommentService(IUnitOfWork unitOfWork, IGameService gameService)
        {
            _unitOfWork = unitOfWork;
            _gameService = gameService;
        }

        public Comment LeaveCommentToGame(string gameKey, Comment comment)
        {
            var game = _gameService.GetGameByKey(gameKey);

            comment.GameId = game.Id;

            comment = _unitOfWork.Comments.Create(comment);
            _unitOfWork.Commit();

            return comment;
        }

        public IEnumerable<Comment> GetAllCommentsByGameKey(string gameKey)
        {
            var comments = _unitOfWork.Comments
                .GetMany(
                    c => c.ReplyToId == null && c.Game.Key == gameKey,
                    $"{nameof(Comment.Game)}",
                    $"{nameof(Comment.ReplyTo)}",
                    $"{nameof(Comment.Replies)}");

            return comments;
        }
    }
}
