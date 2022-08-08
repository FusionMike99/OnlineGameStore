using System.Threading.Tasks;
using OnlineGameStore.BLL.Entities;

namespace OnlineGameStore.BLL.Repositories.GameStore
{
    public interface IPublisherRepository : IGenericRepository<Publisher>
    {
        Task<Publisher> GetByName(string companyName,
            bool includeDeleted = false,
            params string[] includeProperties);
    }
}