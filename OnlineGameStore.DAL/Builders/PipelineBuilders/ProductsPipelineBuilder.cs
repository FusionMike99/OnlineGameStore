using System;
using System.Linq.Expressions;
using OnlineGameStore.DAL.Builders.PipelineBuilders.Interfaces;
using OnlineGameStore.DAL.Entities.Northwind;
using OnlineGameStore.DAL.Pipelines;
using OnlineGameStore.DAL.Pipelines.Filters.Products;
using OnlineGameStore.DomainModels.Models;

namespace OnlineGameStore.DAL.Builders.PipelineBuilders
{
    public class ProductsPipelineBuilder : IProductsPipelineBuilder
    {
        public Pipeline<SortFilterGameModel, Expression<Func<ProductEntity,bool>>> CreatePipeline()
        {
            var productsFilterPipeline = new ProductsFilterPipeline()
                .Register(new ProductsByCategoriesFilter())
                .Register(new ProductsBySuppliersFilter())
                .Register(new ProductsByPriceRangeFilter())
                .Register(new ProductsByNameFilter());

            return productsFilterPipeline;
        }
    }
}