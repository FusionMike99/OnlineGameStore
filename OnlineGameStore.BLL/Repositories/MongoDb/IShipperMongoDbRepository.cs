using System.Threading.Tasks;
using OnlineGameStore.BLL.Entities.Northwind;

namespace OnlineGameStore.BLL.Repositories.MongoDb
{
    public interface IShipperMongoDbRepository : IMongoDbRepository<ShipperEntity>
    {
        Task<ShipperEntity> GetByShipperIdAsync(int shipperId);
    }
}