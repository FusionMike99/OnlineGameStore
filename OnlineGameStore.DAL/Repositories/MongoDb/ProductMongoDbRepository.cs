using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using OnlineGameStore.DAL.Builders.PipelineBuilders.Interfaces;
using OnlineGameStore.DAL.Entities.Northwind;
using OnlineGameStore.DAL.Repositories.MongoDb.Interfaces;
using OnlineGameStore.DomainModels;
using OnlineGameStore.DomainModels.Constants;
using OnlineGameStore.DomainModels.Models;
using OnlineGameStore.ExtensionsUtility.Extensions;

namespace OnlineGameStore.DAL.Repositories.MongoDb
{
    public class ProductMongoDbRepository : MongoDbRepository<ProductEntity>,
        IProductMongoDbRepository
    {
        private readonly ISupplierMongoDbRepository _supplierMongoDbRepository;
        private readonly IProductsPipelineBuilder _productsPipelineBuilder;

        public ProductMongoDbRepository(IMongoDatabase database,
            ILoggerFactory loggerFactory,
            ISupplierMongoDbRepository supplierMongoDbRepository,
            IProductsPipelineBuilder productsPipelineBuilder) : base(database, loggerFactory)
        {
            _supplierMongoDbRepository = supplierMongoDbRepository;
            _productsPipelineBuilder = productsPipelineBuilder;
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
            var products = Query;
            
            var predicate = await GetNorthwindPredicate(sortFilterModel);
            if (predicate != null)
            {
                products = products.Where(predicate);
            }
            var productsList = await products.ToListAsync();

            return productsList;
        }

        private async Task SetGameKeyAndDateAddedPerUnitAsync(ProductEntity product)
        {
            product.Key ??= product.Name.ToKebabCase();
            await UpdateAsync(product);
            product.DateAdded ??= Constants.AddedAtDefault;
        }
        
        private async Task<Expression<Func<ProductEntity, bool>>> GetNorthwindPredicate(SortFilterGameModel model)
        {
            var productsFilterPipeline = _productsPipelineBuilder.CreatePipeline();
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