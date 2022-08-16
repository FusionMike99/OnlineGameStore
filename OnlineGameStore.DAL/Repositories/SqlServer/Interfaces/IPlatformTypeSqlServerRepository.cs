using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineGameStore.DAL.Entities;

namespace OnlineGameStore.DAL.Repositories.SqlServer.Interfaces
{
    public interface IPlatformTypeSqlServerRepository : ISqlServerRepository<PlatformTypeEntity>
    {
        Task<IEnumerable<string>> GetIdsByTypesAsync(IEnumerable<string> types);

        Task<PlatformTypeEntity> GetByTypeAsync(string type);
        
        Task<PlatformTypeEntity> GetByTypeIncludeDeletedAsync(string type);
    }
}