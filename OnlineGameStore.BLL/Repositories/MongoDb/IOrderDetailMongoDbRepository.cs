using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Entities.Northwind;

namespace OnlineGameStore.BLL.Repositories.MongoDb
{
    public interface IOrderDetailMongoDbRepository : IMongoDbRepository<OrderDetailMongoDbEntity>
    {
        Task<IEnumerable<OrderDetailMongoDbEntity>> GetManyByOrderIdAsync(int orderId);
    }
}