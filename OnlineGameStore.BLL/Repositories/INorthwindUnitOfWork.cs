using MongoDB.Bson;
using OnlineGameStore.BLL.Entities.Northwind;
using OnlineGameStore.BLL.Models;

namespace OnlineGameStore.BLL.Repositories
{
    public interface INorthwindUnitOfWork
    {
        INorthwindGenericRepository<NorthwindCategory, ObjectId> Categories { get; }
        
        INorthwindGenericRepository<NorthwindOrder, ObjectId> Orders { get; }
        
        INorthwindGenericRepository<NorthwindOrderDetail, ObjectId> OrderDetails { get; }
        
        INorthwindGenericRepository<NorthwindProduct, ObjectId> Products { get; }
        
        INorthwindGenericRepository<NorthwindShipper, ObjectId> Shippers { get; }

        INorthwindGenericRepository<NorthwindSupplier, ObjectId> Suppliers { get; }
        
        INorthwindGenericRepository<LogModel, ObjectId> Logs { get; }
    }
}