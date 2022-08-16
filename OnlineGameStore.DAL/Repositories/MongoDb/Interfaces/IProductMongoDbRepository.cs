using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.DAL.Entities.Northwind;
using OnlineGameStore.DomainModels.Models;

namespace OnlineGameStore.DAL.Repositories.MongoDb.Interfaces
{
    public interface IProductMongoDbRepository : IMongoDbRepository<ProductEntity>
    {
        Task<ProductEntity> GetByKeyAsync(string gameKey);

        Task<IEnumerable<ProductEntity>> SetGameKeyAndDateAddedAsync(List<ProductEntity> products);
        
        Task<IEnumerable<ProductEntity>> GetAllByFilterAsync(SortFilterGameModel sortFilterModel);
    }
}