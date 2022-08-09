using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Models.General;

namespace OnlineGameStore.BLL.Repositories
{
    public interface IShipperRepository
    {
        Task<IEnumerable<ShipperModel>> GetAllAsync();
    }
}