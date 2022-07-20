using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using OnlineGameStore.BLL.Entities.Northwind;
using OnlineGameStore.BLL.Models;
using OnlineGameStore.BLL.Repositories;

namespace OnlineGameStore.DAL.Repositories
{
    public class NorthwindUnitOfWork : INorthwindUnitOfWork
    {
        private readonly IMongoDatabase _database;
        private NorthwindGenericRepository<NorthwindCategory, ObjectId> _categoriesRepository;
        private NorthwindGenericRepository<NorthwindOrder, ObjectId> _ordersRepository;
        private NorthwindGenericRepository<NorthwindOrderDetail, ObjectId> _orderDetailsRepository;
        private NorthwindGenericRepository<NorthwindProduct, ObjectId> _productsRepository;
        private NorthwindGenericRepository<NorthwindShipper, ObjectId> _shippersRepository;
        private NorthwindGenericRepository<NorthwindSupplier, ObjectId> _suppliersRepository;
        private NorthwindGenericRepository<LogModel, ObjectId> _logsRepository;
        
        public NorthwindUnitOfWork(string connectionString)
        {
            var connection = new MongoUrlBuilder(connectionString);
            var clientSettings = MongoClientSettings.FromConnectionString(connectionString);
            clientSettings.LinqProvider = LinqProvider.V3;
            var client = new MongoClient(clientSettings);
            _database = client.GetDatabase(connection.DatabaseName);
        }

        public INorthwindGenericRepository<NorthwindCategory, ObjectId> Categories =>
            _categoriesRepository ??= new NorthwindGenericRepository<NorthwindCategory, ObjectId>(_database);
        
        public INorthwindGenericRepository<NorthwindOrder, ObjectId> Orders =>
            _ordersRepository ??= new NorthwindGenericRepository<NorthwindOrder, ObjectId>(_database);
        
        public INorthwindGenericRepository<NorthwindOrderDetail, ObjectId> OrderDetails =>
            _orderDetailsRepository ??= new NorthwindGenericRepository<NorthwindOrderDetail, ObjectId>(_database);
        
        public INorthwindGenericRepository<NorthwindProduct, ObjectId> Products =>
            _productsRepository ??= new NorthwindGenericRepository<NorthwindProduct, ObjectId>(_database);
        
        public INorthwindGenericRepository<NorthwindShipper, ObjectId> Shippers =>
            _shippersRepository ??= new NorthwindGenericRepository<NorthwindShipper, ObjectId>(_database);
        
        public INorthwindGenericRepository<NorthwindSupplier, ObjectId> Suppliers =>
            _suppliersRepository ??= new NorthwindGenericRepository<NorthwindSupplier, ObjectId>(_database);

        public INorthwindGenericRepository<LogModel, ObjectId> Logs =>
            _logsRepository ??= new NorthwindGenericRepository<LogModel, ObjectId>(_database);
    }
}