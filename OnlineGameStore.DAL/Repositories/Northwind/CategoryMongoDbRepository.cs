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
    public class CategoryMongoDbRepository : MongoDbRepository<CategoryEntity>,
        ICategoryMongoDbRepository
    {
        public CategoryMongoDbRepository(IMongoDatabase database, ILoggerFactory loggerFactory) : base(database, loggerFactory)
        {
        }

        public async Task<IEnumerable<string>> GetIdsByNamesAsync(IEnumerable<string> categoriesNames)
        {
            Expression<Func<CategoryEntity, bool>> predicate = c => categoriesNames.Contains(c.Name);
            var categoryIds = await Query.Where(predicate)
                .Select(c => c.Id.ToString())
                .ToListAsync();

            return categoryIds;
        }

        public async Task<CategoryEntity> GetByCategoryIdAsync(int categoryId)
        {
            Expression<Func<CategoryEntity, bool>> predicate = c => c.CategoryId == categoryId;
            var category = await Query.FirstOrDefaultAsync(predicate);

            return category;
        }
    }
}