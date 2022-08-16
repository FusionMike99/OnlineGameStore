using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Entities.Northwind;
using OnlineGameStore.BLL.Models;

namespace OnlineGameStore.BLL.Repositories.MongoDb
{
    public interface IProductMongoDbRepository : IMongoDbRepository<NorthwindProduct>
    {
        Task<NorthwindProduct> GetByKeyAsync(string gameKey);

        Task<IEnumerable<NorthwindProduct>> SetGameKeyAndDateAddedAsync(List<NorthwindProduct> products);
        
        Task<IEnumerable<NorthwindProduct>> GetAllByFilterAsync(SortFilterGameModel sortFilterModel);
    }
}