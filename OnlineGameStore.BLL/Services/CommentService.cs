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

        public async Task<CommentModel> LeaveCommentToGameAsync(string gameKey, CommentModel comment)
        {
            var game = await _gameService.GetGameByKeyAsync(gameKey);

            comment.GameId = game.Id;

            await _commentRepository.CreateAsync(comment);

            return comment;
        }

        public async Task<CommentModel> GetCommentByIdAsync(Guid commentId)
        {
            var comment = await _commentRepository.GetByIdAsync(commentId,
                includeProperties: $"{nameof(CommentEntity.Replies)}");

            return comment;
        }

        public async Task<IEnumerable<CommentModel>> GetAllCommentsByGameKeyAsync(string gameKey)
        {
            var comments = await _commentRepository.GetAllByGameKeyAsync(gameKey,
                includeProperties: $"{nameof(CommentEntity.Replies)}");

            var parentComments = comments.Where(c => !c.ReplyToId.HasValue).ToList();

            return parentComments;
        }

        public async Task<CommentModel> EditCommentAsync(CommentModel comment)
        {
            await _commentRepository.UpdateAsync(comment);

            return comment;
        }

        public async Task DeleteCommentAsync(Guid commentId)
        {
            var comment = await _commentRepository.GetByIdAsync(commentId);

            if (comment == null)
            {
                var exception = new InvalidOperationException("Comment has not been found");

                _logger.LogError(exception, @"Service: {Service}; Method: {Method}. 
                    Deleting comment with id {Id} unsuccessfully", nameof(CommentService), nameof(DeleteCommentAsync),
                    commentId);

                throw exception;
            }

            await _commentRepository.DeleteAsync(comment);
        }
    }
}