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

        Task<TEntity> GetByIdAsync(Guid id);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> UpdateAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);
    }
}