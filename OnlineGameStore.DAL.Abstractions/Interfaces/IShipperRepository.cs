using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.DomainModels.Models.General;

namespace OnlineGameStore.DAL.Abstractions.Interfaces
{
    public interface IShipperRepository
    {
        Task<IEnumerable<ShipperModel>> GetAllAsync();
    }
}