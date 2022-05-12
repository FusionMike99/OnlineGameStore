using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Services.Contracts;

namespace OnlineGameStore.BLL.Services
{
    public class CommentService : ICommentService
    {
        private readonly IGameService _gameService;
        private readonly ILogger<CommentService> _logger;
        private readonly IUnitOfWork _unitOfWork;

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
            var comments = _unitOfWork.Comments.GetMany(c => c.Game.Key == gameKey,
                    false,
                    $"{nameof(Comment.Game)}",
                    $"{nameof(Comment.Replies)}");

            var parentComments = comments.Where(c => !c.ReplyToId.HasValue).ToList();

            _logger.LogDebug($@"Class: {nameof(CommentService)}; Method: {nameof(GetAllCommentsByGameKey)}.
                    Receiving comments with game key {gameKey} successfully", parentComments);

            return parentComments;
        }
    }
}