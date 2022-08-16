using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Entities.Northwind;
using OnlineGameStore.BLL.Models;

namespace OnlineGameStore.BLL.Repositories.MongoDb
{
    public interface IProductMongoDbRepository : IMongoDbRepository<ProductEntity>
    {
        Task<ProductEntity> GetByKeyAsync(string gameKey);

        Task<IEnumerable<ProductEntity>> SetGameKeyAndDateAddedAsync(List<ProductEntity> products);
        
        Task<IEnumerable<ProductEntity>> GetAllByFilterAsync(SortFilterGameModel sortFilterModel);
    }
}