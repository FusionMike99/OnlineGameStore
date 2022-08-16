using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using OnlineGameStore.BLL.Entities.Northwind;
using OnlineGameStore.BLL.Repositories.MongoDb;

namespace OnlineGameStore.DAL.Repositories.Northwind
{
    public class ShipperMongoDbRepository : MongoDbRepository<ShipperEntity>, IShipperMongoDbRepository
    {
        public ShipperMongoDbRepository(IMongoDatabase database,
            ILoggerFactory loggerFactory) : base(database, loggerFactory)
        {
        }

        public async Task<ShipperEntity> GetByShipperIdAsync(int shipperId)
        {
            Expression<Func<ShipperEntity, bool>> predicate = s => s.ShipperId == shipperId;
            var shipper = await Query.FirstOrDefaultAsync(predicate);

            return shipper;
        }
    }
}