using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using OnlineGameStore.DAL.Entities.Northwind;
using OnlineGameStore.DAL.Repositories.MongoDb.Interfaces;

namespace OnlineGameStore.DAL.Repositories.MongoDb
{
    public class ShipperMongoDbRepository : MongoDbRepository<ShipperEntity>, IShipperMongoDbRepository
    {
        public ShipperMongoDbRepository(IMongoDatabase database,
            ILoggerFactory loggerFactory) : base(database, loggerFactory)
        {
        }

        public async Task<ShipperEntity> GetByShipperIdAsync(int shipperId)
        {
            var shipper = await Query.FirstOrDefaultAsync(s => s.ShipperId == shipperId);

            return shipper;
        }
    }
}