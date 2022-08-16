using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using OnlineGameStore.DAL.Entities.Northwind;
using OnlineGameStore.DAL.Repositories.MongoDb.Interfaces;

namespace OnlineGameStore.DAL.Repositories.MongoDb
{
    public class SupplierMongoDbRepository : MongoDbRepository<SupplierEntity>,
        ISupplierMongoDbRepository
    {
        public SupplierMongoDbRepository(IMongoDatabase database,
            ILoggerFactory loggerFactory) : base(database, loggerFactory)
        {
        }

        public async Task<SupplierEntity> GetByNameAsync(string companyName)
        {
            var supplier = await Query.FirstOrDefaultAsync(s => s.CompanyName == companyName);

            return supplier;
        }

        public async Task<SupplierEntity> GetBySupplierIdAsync(int supplierId)
        {
            var supplier = await Query.FirstOrDefaultAsync(s => s.SupplierId == supplierId);

            return supplier;
        }

        public async Task<IEnumerable<string>> GetIdsByNamesAsync(IEnumerable<string> companiesNames)
        {
            var suppliersIds = await Query.Where(s => companiesNames.Contains(s.CompanyName))
                .Select(s => s.Id.ToString())
                .ToListAsync();

            return suppliersIds;
        }
    }
}