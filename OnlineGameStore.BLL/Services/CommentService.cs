using Microsoft.Extensions.Logging;
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
        private readonly ILogger<CommentService> _logger;

        public CommentService(
            IUnitOfWork unitOfWork,
            IGameService gameService,
            ILogger<CommentService> logger)
        {
            _unitOfWork = unitOfWork;
            _gameService = gameService;
            _logger = logger;
        }

        public Comment LeaveCommentToGame(string gameKey, Comment comment)
        {
            var game = _gameService.GetGameByKey(gameKey);

            comment.GameId = game.Id;

            var leavedComment = _unitOfWork.Comments.Create(comment);
            _unitOfWork.Commit();

            _logger.LogDebug($@"Class: {nameof(CommentService)}; Method: {nameof(LeaveCommentToGame)}.
                    Leaving comment with id {leavedComment.Id} successfully", leavedComment);

            return leavedComment;
        }

        public IEnumerable<Comment> GetAllCommentsByGameKey(string gameKey)
        {
            var comments = _unitOfWork.Comments
                .GetMany(predicate: c => c.Game.Key == gameKey && !c.ReplyToId.HasValue,
                    includeDeleteEntities: false,
                    $"{nameof(Comment.Game)}",
                    $"{nameof(Comment.ReplyTo)}",
                    $"{nameof(Comment.Replies)}");

            _logger.LogDebug($@"Class: {nameof(CommentService)}; Method: {nameof(GetAllCommentsByGameKey)}.
                    Receiving comments with game key {gameKey} successfully", comments);

            return comments;
        }
    }
}
