using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.DAL.Entities;

namespace OnlineGameStore.DAL.Repositories.SqlServer.Interfaces
{
    public interface ISqlServerRepository<TEntity>
        where TEntity : BaseEntity
    {
        Task<TEntity> CreateAsync(TEntity entity);

        Task<TEntity> GetByIdAsync(Guid id, bool includeDeleted = false, params string[] includeProperties);

        Task<IEnumerable<TEntity>> GetAllAsync(bool includeDeleted = false,
            params string[] includeProperties);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);
    }
}