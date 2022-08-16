using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineGameStore.DAL.Data;
using OnlineGameStore.DAL.Entities;
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