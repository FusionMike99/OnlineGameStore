using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Models.General;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Repositories.GameStore;

namespace OnlineGameStore.DAL.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IGameStoreCommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public CommentRepository(IGameStoreCommentRepository commentRepository,
            IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(CommentModel commentModel)
        {
            var comment = _mapper.Map<CommentEntity>(commentModel);
            var createdComment = await _commentRepository.CreateAsync(comment);
            commentModel.Id = createdComment.Id;
        }

        public async Task UpdateAsync(CommentModel commentModel)
        {
            var comment = _mapper.Map<CommentEntity>(commentModel);
            await _commentRepository.UpdateAsync(comment);
        }

        public async Task DeleteAsync(CommentModel commentModel)
        {
            var comment = _mapper.Map<CommentEntity>(commentModel);
            await _commentRepository.DeleteAsync(comment);
        }

        public async Task<CommentModel> GetByIdAsync(Guid id,
            bool includeDeleted = false,
            params string[] includeProperties)
        {
            var comment = await _commentRepository.GetByIdAsync(id, includeDeleted, includeProperties);
            var mappedComment = _mapper.Map<CommentModel>(comment);

            return mappedComment;
        }

        public async Task<IEnumerable<CommentModel>> GetAllByGameKeyAsync(string gameKey,
            bool includeDeleted = false,
            params string[] includeProperties)
        {
            var comments = await _commentRepository
                .GetAllByGameKeyAsync(gameKey, includeDeleted, includeProperties);
            var mappedComments = _mapper.Map<IEnumerable<CommentModel>>(comments);

            return mappedComments;
        }
    }
}