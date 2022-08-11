using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using OnlineGameStore.BLL.Entities.Northwind;
using OnlineGameStore.BLL.Repositories.Northwind;

namespace OnlineGameStore.DAL.Repositories.Northwind
{
    public class NorthwindShipperRepository : NorthwindGenericRepository<NorthwindShipper>, INorthwindShipperRepository
    {
        public NorthwindShipperRepository(IMongoDatabase database,
            ILoggerFactory loggerFactory) : base(database, loggerFactory)
        {
        }

        public async Task<NorthwindShipper> GetByShipperId(int shipperId)
        {
            Expression<Func<NorthwindShipper, bool>> predicate = s => s.ShipperId == shipperId;

            return await GetFirst(predicate);
        }
    }
}