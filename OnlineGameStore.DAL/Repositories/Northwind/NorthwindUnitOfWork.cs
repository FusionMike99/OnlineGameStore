using MongoDB.Driver;
using MongoDB.Driver.Linq;
using OnlineGameStore.BLL.Entities.Northwind;
using OnlineGameStore.BLL.Repositories.Northwind;

namespace OnlineGameStore.DAL.Repositories.Northwind
{
    public class NorthwindUnitOfWork : INorthwindUnitOfWork
    {
        private readonly IMongoDatabase _database;
        private NorthwindGenericRepository<NorthwindCategory> _categoriesRepository;
        private NorthwindGenericRepository<NorthwindOrder> _ordersRepository;
        private NorthwindGenericRepository<NorthwindOrderDetail> _orderDetailsRepository;
        private NorthwindGenericRepository<NorthwindProduct> _productsRepository;
        private NorthwindGenericRepository<NorthwindShipper> _shippersRepository;
        private NorthwindGenericRepository<NorthwindSupplier> _suppliersRepository;
        
        public NorthwindUnitOfWork(string connectionString)
        {
            var connection = new MongoUrlBuilder(connectionString);
            var clientSettings = MongoClientSettings.FromConnectionString(connectionString);
            clientSettings.LinqProvider = LinqProvider.V3;
            var client = new MongoClient(clientSettings);
            _database = client.GetDatabase(connection.DatabaseName);
        }

        public INorthwindGenericRepository<NorthwindCategory> Categories =>
            _categoriesRepository ??= new NorthwindGenericRepository<NorthwindCategory>(_database);
        
        public INorthwindGenericRepository<NorthwindOrder> Orders =>
            _ordersRepository ??= new NorthwindGenericRepository<NorthwindOrder>(_database);
        
        public INorthwindGenericRepository<NorthwindOrderDetail> OrderDetails =>
            _orderDetailsRepository ??= new NorthwindGenericRepository<NorthwindOrderDetail>(_database);
        
        public INorthwindGenericRepository<NorthwindProduct> Products =>
            _productsRepository ??= new NorthwindGenericRepository<NorthwindProduct>(_database);
        
        public INorthwindGenericRepository<NorthwindShipper> Shippers =>
            _shippersRepository ??= new NorthwindGenericRepository<NorthwindShipper>(_database);
        
        public INorthwindGenericRepository<NorthwindSupplier> Suppliers =>
            _suppliersRepository ??= new NorthwindGenericRepository<NorthwindSupplier>(_database);
    }
}