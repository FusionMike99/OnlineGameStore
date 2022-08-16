using System.Threading.Tasks;
using OnlineGameStore.DAL.Entities.Northwind;

namespace OnlineGameStore.DAL.Repositories.MongoDb.Interfaces
{
    public interface IShipperMongoDbRepository : IMongoDbRepository<ShipperEntity>
    {
        Task<ShipperEntity> GetByShipperIdAsync(int shipperId);
    }
}