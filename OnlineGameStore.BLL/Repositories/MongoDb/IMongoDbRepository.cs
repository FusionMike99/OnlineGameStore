using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Entities.Northwind;

namespace OnlineGameStore.BLL.Repositories.MongoDb
{
    public interface IMongoDbRepository<TEntity>
        where TEntity : MongoBaseEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> UpdateAsync(TEntity entity);
    }
}