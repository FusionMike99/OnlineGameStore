using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.DAL.Entities.Northwind;

namespace OnlineGameStore.DAL.Repositories.MongoDb.Interfaces
{
    public interface ICategoryMongoDbRepository : IMongoDbRepository<CategoryEntity>
    {
        Task<IEnumerable<string>> GetIdsByNamesAsync(IEnumerable<string> categoriesNames);
        
        Task<CategoryEntity> GetByCategoryIdAsync(int categoryId);
    }
}