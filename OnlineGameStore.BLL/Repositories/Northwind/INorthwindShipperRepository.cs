using System.Threading.Tasks;
using OnlineGameStore.BLL.Entities.Northwind;

namespace OnlineGameStore.BLL.Repositories.Northwind
{
    public interface INorthwindShipperRepository : INorthwindGenericRepository<NorthwindShipper>
    {
        Task<NorthwindShipper> GetByShipperId(int shipperId);
    }
}