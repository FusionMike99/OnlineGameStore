﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using OnlineGameStore.BLL.Entities.Northwind;
using OnlineGameStore.BLL.Repositories.Northwind;

namespace OnlineGameStore.DAL.Repositories.Northwind
{
    public class NorthwindSupplierRepository : NorthwindGenericRepository<NorthwindSupplier>,
        INorthwindSupplierRepository
    {
        public NorthwindSupplierRepository(IMongoDatabase database,
            ILoggerFactory loggerFactory) : base(database, loggerFactory)
        {
        }

        public async Task<NorthwindSupplier> GetByNameAsync(string companyName)
        {
            Expression<Func<NorthwindSupplier, bool>> predicate = s => s.CompanyName == companyName;

            return await GetFirst(predicate);
        }

        public async Task<NorthwindSupplier> GetBySupplierIdAsync(int supplierId)
        {
            Expression<Func<NorthwindSupplier, bool>> predicate = c => c.SupplierId == supplierId;

            return await GetFirst(predicate);
        }

        public async Task<IEnumerable<string>> GetIdsByNamesAsync(IEnumerable<string> companiesNames)
        {
            var suppliers = await GetMany(s => companiesNames.Contains(s.CompanyName));

            var suppliersIds = suppliers.Select(s => s.Id.ToString());

            return suppliersIds;
        }
    }
}