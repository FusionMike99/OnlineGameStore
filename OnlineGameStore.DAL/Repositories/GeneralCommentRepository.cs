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
    public class GeneralCommentRepository : IGeneralCommentRepository
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public GeneralCommentRepository(ICommentRepository commentRepository,
            IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(CommentModel commentModel)
        {
            var comment = _mapper.Map<Comment>(commentModel);

            var createdComment = await _commentRepository.Create(comment);

            commentModel.Id = createdComment.Id.ToString();
        }

        public async Task UpdateAsync(CommentModel commentModel)
        {
            var comment = _mapper.Map<Comment>(commentModel);

            await _commentRepository.Update(comment);
        }

        public async Task DeleteAsync(CommentModel commentModel)
        {
            var comment = _mapper.Map<Comment>(commentModel);

            await _commentRepository.Delete(comment);
        }

        public async Task<CommentModel> GetByIdAsync(string id,
            bool includeDeleted = false,
            params string[] includeProperties)
        {
            var isParsed = Guid.TryParse(id, out var commentGuid);

            if (!isParsed)
            {
                return null;
            }
            
            var comment = await _commentRepository.GetById(commentGuid, includeDeleted, includeProperties);
            var mappedComment = _mapper.Map<CommentModel>(comment);

            return mappedComment;
        }

        public async Task<IEnumerable<CommentModel>> GetAllByGameKeyAsync(string gameKey,
            bool includeDeleted = false,
            params string[] includeProperties)
        {
            var comments = await _commentRepository
                .GetAllByGameKey(gameKey, includeDeleted, includeProperties);
            var mappedComments = _mapper.Map<IEnumerable<CommentModel>>(comments);

            return mappedComments;
        }
    }
}