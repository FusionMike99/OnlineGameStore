using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineGameStore.DAL.Data;
using OnlineGameStore.DAL.Entities;
using OnlineGameStore.DAL.Repositories.SqlServer.Extensions;
using OnlineGameStore.DAL.Repositories.SqlServer.Interfaces;

namespace OnlineGameStore.DAL.Repositories.SqlServer
{
    public class PlatformTypeSqlServerRepository : SqlServerRepository<PlatformTypeEntity>, IPlatformTypeSqlServerRepository
    {
        public PlatformTypeSqlServerRepository(StoreDbContext context, ILoggerFactory logger) : base(context, logger)
        {
        }

        public async Task<IEnumerable<string>> GetIdsByTypesAsync(IEnumerable<string> types)
        {
            var platformTypesIds = await Query.Where(s => types.Contains(s.Type))
                .Select(g => g.Id.ToString()).ToListAsync();

            return platformTypesIds;
        }

        public async Task<PlatformTypeEntity> GetByTypeAsync(string type)
        {
            var platformType = await Query.FirstOrDefaultAsync(pt => pt.Type == type);

            return platformType;
        }

        public async Task<PlatformTypeEntity> GetByTypeIncludeDeletedAsync(string type)
        {
            var platformType = await Query.IncludeDeleted().FirstOrDefaultAsync(pt => pt.Type == type);

            return platformType;
        }
    }
}