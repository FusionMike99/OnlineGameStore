using MongoDB.Bson;
using OnlineGameStore.BLL.Entities.Northwind;
using OnlineGameStore.BLL.Models;

namespace OnlineGameStore.BLL.Repositories
{
    public interface INorthwindUnitOfWork
    {
        INorthwindGenericRepository<NorthwindCategory> Categories { get; }
        
        INorthwindGenericRepository<NorthwindOrder> Orders { get; }
        
        INorthwindGenericRepository<NorthwindOrderDetail> OrderDetails { get; }
        
        INorthwindGenericRepository<NorthwindProduct> Products { get; }
        
        INorthwindGenericRepository<NorthwindShipper> Shippers { get; }

        INorthwindGenericRepository<NorthwindSupplier> Suppliers { get; }
    }
}