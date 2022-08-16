using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.DomainModels.Models.General;

namespace OnlineGameStore.BLL.Services.Interfaces
{
    public interface IShipperService
    {
        Task<IEnumerable<ShipperModel>> GetAllShippersAsync();
    }
}