using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Repositories.SqlServer;
using OnlineGameStore.DAL.Data;

namespace OnlineGameStore.DAL.Repositories.GameStore
{
    public class PlatformTypeSqlServerRepository : SqlServerRepository<PlatformTypeEntity>, IPlatformTypeSqlServerRepository
    {
        public PlatformTypeSqlServerRepository(StoreDbContext context, ILoggerFactory logger) : base(context, logger)
        {
        }

        public async Task<IEnumerable<string>> GetIdsByTypesAsync(IEnumerable<string> types)
        {
            Expression<Func<PlatformTypeEntity, bool>> predicate = s => types.Contains(s.Type);
            var platformTypesIds = await IncludeProperties().Where(predicate)
                .Select(g => g.Id.ToString()).ToListAsync();

            return platformTypesIds;
        }

        public async Task<PlatformTypeEntity> GetByTypeAsync(string type, bool includeDeleted = false, params string[] includeProperties)
        {
            Expression<Func<PlatformTypeEntity, bool>> predicate = pt => pt.Type == type;
            var platformType = await IncludeProperties(includeDeleted, includeProperties).SingleOrDefaultAsync(predicate);

            return platformType;
        }
    }
}