using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using OnlineGameStore.BLL.Entities.Northwind;
using OnlineGameStore.BLL.Repositories.MongoDb;

namespace OnlineGameStore.DAL.Repositories.Northwind
{
    public class OrderDetailMongoDbRepository : MongoDbRepository<OrderDetailMongoDbEntity>,
        IOrderDetailMongoDbRepository
    {
        public OrderDetailMongoDbRepository(IMongoDatabase database, ILoggerFactory loggerFactory)
            : base(database, loggerFactory)
        {
        }

        public async Task<IEnumerable<OrderDetailMongoDbEntity>> GetManyByOrderIdAsync(int orderId)
        {
            Expression<Func<OrderDetailMongoDbEntity, bool>> predicate = od => od.OrderId == orderId;
            var orderDetails = await Query.Where(predicate).ToListAsync();

            return orderDetails;
        }
    }
}