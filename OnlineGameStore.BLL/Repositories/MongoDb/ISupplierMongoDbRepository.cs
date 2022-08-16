using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Entities.Northwind;

namespace OnlineGameStore.BLL.Repositories.MongoDb
{
    public interface ISupplierMongoDbRepository : IMongoDbRepository<SupplierEntity>
    {
        Task<SupplierEntity> GetByNameAsync(string companyName);
        
        Task<SupplierEntity> GetBySupplierIdAsync(int supplierId);

        Task<IEnumerable<string>> GetIdsByNamesAsync(IEnumerable<string> companiesNames);
    }
}