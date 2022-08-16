using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using OnlineGameStore.BLL.Entities.Northwind;
using OnlineGameStore.BLL.Repositories.MongoDb;

namespace OnlineGameStore.DAL.Repositories.Northwind
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
            Expression<Func<SupplierEntity, bool>> predicate = s => s.CompanyName == companyName;
            var supplier = await Query.FirstOrDefaultAsync(predicate);

            return supplier;
        }

        public async Task<SupplierEntity> GetBySupplierIdAsync(int supplierId)
        {
            Expression<Func<SupplierEntity, bool>> predicate = c => c.SupplierId == supplierId;
            var supplier = await Query.FirstOrDefaultAsync(predicate);

            return supplier;
        }

        public async Task<IEnumerable<string>> GetIdsByNamesAsync(IEnumerable<string> companiesNames)
        {
            Expression<Func<SupplierEntity, bool>> predicate = s => companiesNames.Contains(s.CompanyName);
            var suppliersIds = await Query.Where(predicate)
                .Select(s => s.Id.ToString())
                .ToListAsync();

            return suppliersIds;
        }
    }
}