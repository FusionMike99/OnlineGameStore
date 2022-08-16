using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Entities.Northwind;

namespace OnlineGameStore.BLL.Repositories.MongoDb
{
    public interface ISupplierMongoDbRepository : IMongoDbRepository<NorthwindSupplier>
    {
        Task<NorthwindSupplier> GetByNameAsync(string companyName);
        
        Task<NorthwindSupplier> GetBySupplierIdAsync(int supplierId);

        Task<IEnumerable<string>> GetIdsByNamesAsync(IEnumerable<string> companiesNames);
    }
}