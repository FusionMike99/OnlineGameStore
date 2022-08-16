using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using OnlineGameStore.BLL.Builders.PipelineBuilders;
using OnlineGameStore.BLL.Entities.Northwind;
using OnlineGameStore.BLL.Models;
using OnlineGameStore.BLL.Repositories.Northwind;
using OnlineGameStore.BLL.Utils;

namespace OnlineGameStore.DAL.Repositories.Northwind
{
    public class NorthwindProductRepository : NorthwindGenericRepository<NorthwindProduct>,
        INorthwindProductRepository
    {
        private readonly INorthwindSupplierRepository _supplierRepository;

        public NorthwindProductRepository(IMongoDatabase database,
            ILoggerFactory loggerFactory,
            INorthwindSupplierRepository supplierRepository) : base(database, loggerFactory)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<NorthwindProduct> GetByKeyAsync(string gameKey)
        {
            Expression<Func<NorthwindProduct, bool>> predicate = p => p.Key == gameKey;
            var product = await Query.FirstOrDefaultAsync(predicate);

            return product;
        }

        public async Task<IEnumerable<NorthwindProduct>> SetGameKeyAndDateAddedAsync(List<NorthwindProduct> products)
        {
            foreach (var product in products)
            {
                await SetGameKeyAndDateAddedPerUnitAsync(product);
            }

            return products;
        }

        public async Task<IEnumerable<NorthwindProduct>> GetAllByFilterAsync(SortFilterGameModel sortFilterModel)
        {
            var predicate = await GetNorthwindPredicate(sortFilterModel);
            var products = await Query.Where(predicate).ToListAsync();

            return products;
        }

        private async Task SetGameKeyAndDateAddedPerUnitAsync(NorthwindProduct product)
        {
            product.Key ??= product.Name.ToKebabCase();
            await UpdateAsync(product);
            product.DateAdded ??= Constants.AddedAtDefault;
        }
        
        private async Task<Expression<Func<NorthwindProduct, bool>>> GetNorthwindPredicate(SortFilterGameModel model)
        {
            var productsFilterPipeline = ProductsPipelineBuilder.CreatePipeline();
            await SetSuppliersIds(model);
            var predicate = productsFilterPipeline.Process(model);

            return predicate;
        }
        
        private async Task SetSuppliersIds(SortFilterGameModel model)
        {
            if (model != null && model.SelectedPublishers?.Any() == false)
            {
                var supplierIds = await _supplierRepository.GetIdsByNamesAsync(model.SelectedPublishers);
                model.SelectedSuppliers = supplierIds.ToList();
            }
        }
    }
}