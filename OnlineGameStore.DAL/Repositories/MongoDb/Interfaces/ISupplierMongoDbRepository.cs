using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.DAL.Entities.Northwind;

namespace OnlineGameStore.DAL.Repositories.MongoDb.Interfaces
{
    public interface ISupplierMongoDbRepository : IMongoDbRepository<SupplierEntity>
    {
        Task<SupplierEntity> GetByNameAsync(string companyName);
        
        Task<SupplierEntity> GetBySupplierIdAsync(int supplierId);

        Task<IEnumerable<string>> GetIdsByNamesAsync(IEnumerable<string> companiesNames);
    }
}