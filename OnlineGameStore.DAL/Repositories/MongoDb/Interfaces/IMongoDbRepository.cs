using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.DAL.Entities.Northwind;

namespace OnlineGameStore.DAL.Repositories.MongoDb.Interfaces
{
    public interface IMongoDbRepository<TEntity>
        where TEntity : MongoBaseEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> UpdateAsync(TEntity entity);
    }
}