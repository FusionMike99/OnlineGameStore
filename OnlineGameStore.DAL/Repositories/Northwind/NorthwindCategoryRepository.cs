using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using OnlineGameStore.BLL.Entities.Northwind;
using OnlineGameStore.BLL.Repositories.Northwind;

namespace OnlineGameStore.DAL.Repositories.Northwind
{
    public class NorthwindCategoryRepository : NorthwindGenericRepository<NorthwindCategory>,
        INorthwindCategoryRepository
    {
        public NorthwindCategoryRepository(IMongoDatabase database) : base(database)
        {
        }

        public async Task<IEnumerable<string>> GetIdsByNames(IEnumerable<string> categoriesNames)
        {
            var categories = await GetMany(c => categoriesNames.Contains(c.Name));

            var genreIds = categories.Select(c => c.Id.ToString());

            return genreIds;
        }
    }
}