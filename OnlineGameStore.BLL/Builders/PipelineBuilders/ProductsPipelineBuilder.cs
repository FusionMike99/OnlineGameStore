using System;
using System.Linq.Expressions;
using OnlineGameStore.BLL.Entities.Northwind;
using OnlineGameStore.BLL.Models;
using OnlineGameStore.BLL.Pipelines;
using OnlineGameStore.BLL.Pipelines.Filters.Products;

namespace OnlineGameStore.BLL.Builders.PipelineBuilders
{
    public static class ProductsPipelineBuilder
    {
        public static Pipeline<SortFilterGameModel, Expression<Func<ProductEntity,bool>>> CreatePipeline()
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