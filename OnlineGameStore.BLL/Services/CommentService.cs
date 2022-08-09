using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Models.General;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Services.Contracts;

namespace OnlineGameStore.BLL.Services
{
    public class CommentService : ICommentService
    {
        private readonly IGameService _gameService;
        private readonly ILogger<CommentService> _logger;
        private readonly ICommentRepository _commentRepository;

        public CommentService(
            IGameService gameService,
            ILogger<CommentService> logger,
            ICommentRepository commentRepository)
        {
            _gameService = gameService;
            _logger = logger;
            _commentRepository = commentRepository;
        }

        public async Task<CommentModel> LeaveCommentToGame(string gameKey, CommentModel comment)
        {
            var game = _gameService.GetGameByKey(gameKey);

            comment.GameId = game.Id.ToString();

            await _commentRepository.CreateAsync(comment);

            _logger.LogDebug($@"Class: {nameof(CommentService)}; Method: {nameof(LeaveCommentToGame)}.
                    Leaving comment with id {comment.Id} successfully", comment);

            return comment;
        }

        public async Task<CommentModel> GetCommentById(string commentId)
        {
            var comment = await _commentRepository.GetByIdAsync(commentId,
                includeProperties: $"{nameof(Comment.Replies)}");

            return comment;
        }

        public async Task<IEnumerable<CommentModel>> GetAllCommentsByGameKey(string gameKey)
        {
            var comments = await _commentRepository.GetAllByGameKeyAsync(gameKey,
                includeProperties: $"{nameof(Comment.Replies)}");

            var parentComments = comments.Where(c => string.IsNullOrWhiteSpace(c.ReplyToId)).ToList();

            return parentComments;
        }

        public async Task<CommentModel> EditComment(CommentModel comment)
        {
            await _commentRepository.UpdateAsync(comment);

            _logger.LogDebug($@"Class: {nameof(CommentService)}; Method: {nameof(EditComment)}.
                    Editing comment with id {comment.Id} successfully", comment);

            return comment;
        }

        public async Task DeleteComment(string commentId)
        {
            var comment = await _commentRepository.GetByIdAsync(commentId);

            if (comment == null)
            {
                var exception = new InvalidOperationException("Comment has not been found");

                _logger.LogError(exception, $@"Class: {nameof(CommentService)}; Method: {nameof(DeleteComment)}.
                    Deleting comment with id {commentId} unsuccessfully", commentId);

                throw exception;
            }

            await _commentRepository.DeleteAsync(comment);

            _logger.LogDebug($@"Class: {nameof(CommentService)}; Method: {nameof(DeleteComment)}.
                    Deleting comment with id {commentId} successfully", comment);
        }
    }
}