using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Entities.Northwind;
using OnlineGameStore.BLL.Models;

namespace OnlineGameStore.BLL.Repositories.Northwind
{
    public interface INorthwindProductRepository : INorthwindGenericRepository<NorthwindProduct>
    {
        Task<NorthwindProduct> GetByKey(string gameKey);

        Task<IEnumerable<NorthwindProduct>> SetGameKeyAndDateAdded(List<NorthwindProduct> products);
        
        Task<IEnumerable<NorthwindProduct>> GetAllByFilter(SortFilterGameModel sortFilterModel);
    }
}