using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using OnlineGameStore.DAL.Entities.Northwind;
using OnlineGameStore.DAL.Repositories.MongoDb.Interfaces;

namespace OnlineGameStore.DAL.Repositories.MongoDb
{
    public class CategoryMongoDbRepository : MongoDbRepository<CategoryEntity>,
        ICategoryMongoDbRepository
    {
        public CategoryMongoDbRepository(IMongoDatabase database, ILoggerFactory loggerFactory) : base(database, loggerFactory)
        {
        }

        public async Task<IEnumerable<string>> GetIdsByNamesAsync(IEnumerable<string> categoriesNames)
        {
            var categoryIds = await Query.Where(c => categoriesNames.Contains(c.Name))
                .Select(c => c.Id.ToString())
                .ToListAsync();

            return categoryIds;
        }

        public async Task<CategoryEntity> GetByCategoryIdAsync(int categoryId)
        {
            var category = await Query.FirstOrDefaultAsync(c => c.CategoryId == categoryId);

            return category;
        }
    }
}