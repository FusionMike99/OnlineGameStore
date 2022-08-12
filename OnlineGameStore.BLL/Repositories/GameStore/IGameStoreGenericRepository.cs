using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Entities;

namespace OnlineGameStore.BLL.Repositories.GameStore
{
    public interface IGameStoreGenericRepository<TEntity>
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