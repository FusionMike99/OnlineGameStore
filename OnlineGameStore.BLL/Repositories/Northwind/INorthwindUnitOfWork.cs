using OnlineGameStore.BLL.Entities.Northwind;

namespace OnlineGameStore.BLL.Repositories.Northwind
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