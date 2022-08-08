using MongoDB.Driver;
using OnlineGameStore.BLL.Entities.Northwind;
using OnlineGameStore.BLL.Repositories.Northwind;

namespace OnlineGameStore.DAL.Repositories.Northwind
{
    public class NorthwindShipperRepository : NorthwindGenericRepository<NorthwindShipper>, INorthwindShipperRepository
    {
        public NorthwindShipperRepository(IMongoDatabase database) : base(database)
        {
        }
    }
}