﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.DomainModels.Models.General;

namespace OnlineGameStore.DAL.Abstractions.Interfaces
{
    public interface ICommentRepository
    {
        Task CreateAsync(CommentModel commentModel);

        Task UpdateAsync(CommentModel commentModel);

        Task DeleteAsync(CommentModel commentModel);
        
        Task<CommentModel> GetByIdAsync(Guid id);

        Task<IEnumerable<CommentModel>> GetAllByGameKeyAsync(string gameKey);
    }
}