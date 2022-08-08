using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Entities.Northwind;

namespace OnlineGameStore.BLL.Repositories.Northwind
{
    public interface INorthwindCategoryRepository : INorthwindGenericRepository<NorthwindCategory>
    {
        Task<IEnumerable<string>> GetIdsByNames(IEnumerable<string> categoriesNames);
    }
}