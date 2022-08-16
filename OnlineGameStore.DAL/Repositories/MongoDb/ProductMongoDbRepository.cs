using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using OnlineGameStore.DAL.Builders.PipelineBuilders;
using OnlineGameStore.DAL.Entities.Northwind;
using OnlineGameStore.DAL.Repositories.MongoDb.Interfaces;
using OnlineGameStore.DAL.Utils;
using OnlineGameStore.DomainModels;
using OnlineGameStore.DomainModels.Models;
using OnlineGameStore.DomainModels.Utils;

namespace OnlineGameStore.DAL.Repositories.MongoDb
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
            var product = await Query.FirstOrDefaultAsync(p => p.Key == gameKey);

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
            var productsPipelineBuilder = new ProductsPipelineBuilder();
            var productsFilterPipeline = productsPipelineBuilder.CreatePipeline();
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