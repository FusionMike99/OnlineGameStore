using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Entities.Northwind;

namespace OnlineGameStore.BLL.Repositories.MongoDb
{
    public interface ICategoryMongoDbRepository : IMongoDbRepository<CategoryEntity>
    {
        Task<IEnumerable<string>> GetIdsByNamesAsync(IEnumerable<string> categoriesNames);
        
        Task<CategoryEntity> GetByCategoryIdAsync(int categoryId);
    }
}