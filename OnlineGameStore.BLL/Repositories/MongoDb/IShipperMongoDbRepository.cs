using System.Threading.Tasks;
using OnlineGameStore.BLL.Entities.Northwind;

namespace OnlineGameStore.BLL.Repositories.MongoDb
{
    public interface IShipperMongoDbRepository : IMongoDbRepository<NorthwindShipper>
    {
        Task<NorthwindShipper> GetByShipperIdAsync(int shipperId);
    }
}