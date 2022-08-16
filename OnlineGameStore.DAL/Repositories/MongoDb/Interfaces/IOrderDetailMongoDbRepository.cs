using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.DAL.Entities.Northwind;

namespace OnlineGameStore.DAL.Repositories.MongoDb.Interfaces
{
    public interface IOrderDetailMongoDbRepository : IMongoDbRepository<OrderDetailMongoDbEntity>
    {
        Task<IEnumerable<OrderDetailMongoDbEntity>> GetManyByOrderIdAsync(int orderId);
    }
}