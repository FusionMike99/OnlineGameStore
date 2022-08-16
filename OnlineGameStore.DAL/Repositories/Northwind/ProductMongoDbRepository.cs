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
using OnlineGameStore.BLL.Repositories.MongoDb;
using OnlineGameStore.BLL.Utils;

namespace OnlineGameStore.DAL.Repositories.Northwind
{
    public class ProductMongoDbRepository : MongoDbRepository<ProductEntity>,
        IProductMongoDbRepository
    {
        private readonly ISupplierMongoDbRepository _supplierMongoDbRepository;

        public ProductMongoDbRepository(IMongoDatabase database,
            ILoggerFactory loggerFactory,
            ISupplierMongoDbRepository supplierMongoDbRepository) : base(database, loggerFactory)
        {
            _supplierMongoDbRepository = supplierMongoDbRepository;
        }

        public async Task<ProductEntity> GetByKeyAsync(string gameKey)
        {
            Expression<Func<ProductEntity, bool>> predicate = p => p.Key == gameKey;
            var product = await Query.FirstOrDefaultAsync(predicate);

            return product;
        }

        public async Task<IEnumerable<ProductEntity>> SetGameKeyAndDateAddedAsync(List<ProductEntity> products)
        {
            foreach (var product in products)
            {
                await SetGameKeyAndDateAddedPerUnitAsync(product);
            }

            return products;
        }

        public async Task<IEnumerable<ProductEntity>> GetAllByFilterAsync(SortFilterGameModel sortFilterModel)
        {
            var predicate = await GetNorthwindPredicate(sortFilterModel);
            var products = await Query.Where(predicate).ToListAsync();

            return products;
        }

        private async Task SetGameKeyAndDateAddedPerUnitAsync(ProductEntity product)
        {
            product.Key ??= product.Name.ToKebabCase();
            await UpdateAsync(product);
            product.DateAdded ??= Constants.AddedAtDefault;
        }
        
        private async Task<Expression<Func<ProductEntity, bool>>> GetNorthwindPredicate(SortFilterGameModel model)
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
                var supplierIds = await _supplierMongoDbRepository.GetIdsByNamesAsync(model.SelectedPublishers);
                model.SelectedSuppliers = supplierIds.ToList();
            }
        }
    }
}