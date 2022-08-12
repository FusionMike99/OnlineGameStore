using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using OnlineGameStore.BLL.Entities.Northwind;
using OnlineGameStore.BLL.Repositories.Northwind;

namespace OnlineGameStore.DAL.Repositories.Northwind
{
    public class NorthwindOrderDetailRepository : NorthwindGenericRepository<NorthwindOrderDetail>,
        INorthwindOrderDetailRepository
    {
        public NorthwindOrderDetailRepository(IMongoDatabase database, ILoggerFactory loggerFactory)
            : base(database, loggerFactory)
        {
        }

        public async Task<IEnumerable<NorthwindOrderDetail>> GetManyByOrderIdAsync(int orderId)
        {
            Expression<Func<NorthwindOrderDetail, bool>> predicate = od => od.OrderId == orderId;

            return await GetMany(predicate);
        }
    }
}