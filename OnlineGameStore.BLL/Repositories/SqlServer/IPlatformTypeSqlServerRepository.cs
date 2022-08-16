using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Entities;

namespace OnlineGameStore.BLL.Repositories.SqlServer
{
    public interface IPlatformTypeSqlServerRepository : ISqlServerRepository<PlatformTypeEntity>
    {
        Task<IEnumerable<string>> GetIdsByTypesAsync(IEnumerable<string> types);

        Task<PlatformTypeEntity> GetByTypeAsync(string type, bool includeDeleted = false,
            params string[] includeProperties);
    }
}