using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Entities.Northwind;
using OnlineGameStore.BLL.Models;

namespace OnlineGameStore.BLL.Repositories.Northwind
{
    public interface INorthwindOrderRepository : INorthwindGenericRepository<NorthwindOrder>
    {
        Task<IEnumerable<NorthwindOrder>> GetOrdersAsync(FilterOrderModel filterOrderModel = null);
    }
}