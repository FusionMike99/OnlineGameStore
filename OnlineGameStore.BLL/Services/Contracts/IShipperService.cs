using System.Collections.Generic;
using OnlineGameStore.BLL.Entities.Northwind;

namespace OnlineGameStore.BLL.Services.Contracts
{
    public interface IShipperService
    {
        IEnumerable<NorthwindShipper> GetAllShippers();
    }
}