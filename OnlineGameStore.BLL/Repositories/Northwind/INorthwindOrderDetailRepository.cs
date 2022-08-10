using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Entities.Northwind;

namespace OnlineGameStore.BLL.Repositories.Northwind
{
    public interface INorthwindOrderDetailRepository : INorthwindGenericRepository<NorthwindOrderDetail>
    {
        Task<IEnumerable<NorthwindOrderDetail>> GetManyByOrderId(int orderId);
    }
}