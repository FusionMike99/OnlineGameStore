using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using OnlineGameStore.DAL.Abstractions.Interfaces;
using OnlineGameStore.DAL.Entities;
using OnlineGameStore.DAL.Repositories.SqlServer.Interfaces;
using OnlineGameStore.DomainModels.Models.General;

namespace OnlineGameStore.DAL.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ICommentSqlServerRepository _commentSqlServerRepository;
        private readonly IMapper _mapper;

        public CommentRepository(ICommentSqlServerRepository commentSqlServerRepository,
            IMapper mapper)
        {
            _commentSqlServerRepository = commentSqlServerRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(CommentModel commentModel)
        {
            var comment = _mapper.Map<CommentEntity>(commentModel);
            var createdComment = await _commentSqlServerRepository.CreateAsync(comment);
            commentModel.Id = createdComment.Id;
        }

        public async Task UpdateAsync(CommentModel commentModel)
        {
            var comment = _mapper.Map<CommentEntity>(commentModel);
            await _commentSqlServerRepository.UpdateAsync(comment);
        }

        public async Task DeleteAsync(CommentModel commentModel)
        {
            var comment = _mapper.Map<CommentEntity>(commentModel);
            await _commentSqlServerRepository.DeleteAsync(comment);
        }

        public async Task<CommentModel> GetByIdAsync(Guid id,
            bool includeDeleted = false,
            params string[] includeProperties)
        {
            var comment = await _commentSqlServerRepository.GetByIdAsync(id, includeDeleted, includeProperties);
            var mappedComment = _mapper.Map<CommentModel>(comment);

            return mappedComment;
        }

        public async Task<IEnumerable<CommentModel>> GetAllByGameKeyAsync(string gameKey,
            bool includeDeleted = false,
            params string[] includeProperties)
        {
            var comments = await _commentSqlServerRepository
                .GetAllByGameKeyAsync(gameKey, includeDeleted, includeProperties);
            var mappedComments = _mapper.Map<IEnumerable<CommentModel>>(comments);

            return mappedComments;
        }
    }
}