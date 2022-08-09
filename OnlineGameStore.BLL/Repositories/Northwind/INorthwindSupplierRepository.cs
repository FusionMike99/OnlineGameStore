using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Entities.Northwind;

namespace OnlineGameStore.BLL.Repositories.Northwind
{
    public interface INorthwindSupplierRepository : INorthwindGenericRepository<NorthwindSupplier>
    {
        Task<NorthwindSupplier> GetByName(string companyName);
        
        Task<NorthwindSupplier> GetBySupplierId(int supplierId);

        Task<IEnumerable<string>> GetIdsByNames(IEnumerable<string> companiesNames);
    }
}