using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using OnlineGameStore.BLL.Entities.Northwind;
using OnlineGameStore.BLL.Models;
using OnlineGameStore.BLL.Pipelines;
using OnlineGameStore.BLL.Pipelines.Filters.Products;
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

            return await GetFirst(predicate);
        }

        public Task<IEnumerable<NorthwindProduct>> SetGameKeyAndDateAddedAsync(List<NorthwindProduct> products)
        {
            async void Action(NorthwindProduct product)
            {
                product.Key ??= product.Name.ToKebabCase();

                await UpdateAsync(product);

                product.DateAdded ??= Constants.AddedAtDefault;
            }

            products.ForEach(Action);

            return Task.FromResult<IEnumerable<NorthwindProduct>>(products);
        }

        public async Task<IEnumerable<NorthwindProduct>> GetAllByFilterAsync(SortFilterGameModel sortFilterModel)
        {
            var predicate = await GetNorthwindPredicate(sortFilterModel);

            return await GetMany(predicate);
        }
        
        private async Task<Expression<Func<NorthwindProduct, bool>>> GetNorthwindPredicate(SortFilterGameModel model)
        {
            var productsFilterPipeline = new ProductsFilterPipeline()
                .Register(new ProductsByCategoriesFilter())
                .Register(new ProductsBySuppliersFilter())
                .Register(new ProductsByPriceRangeFilter())
                .Register(new ProductsByNameFilter());

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