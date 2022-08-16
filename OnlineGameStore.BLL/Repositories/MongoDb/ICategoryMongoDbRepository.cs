using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Entities.Northwind;

namespace OnlineGameStore.BLL.Repositories.MongoDb
{
    public interface ICategoryMongoDbRepository : IMongoDbRepository<NorthwindCategory>
    {
        Task<IEnumerable<string>> GetIdsByNamesAsync(IEnumerable<string> categoriesNames);
        
        Task<NorthwindCategory> GetByCategoryIdAsync(int categoryId);
    }
}