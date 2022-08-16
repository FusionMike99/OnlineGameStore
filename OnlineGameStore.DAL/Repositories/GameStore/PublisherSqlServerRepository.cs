using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Repositories.SqlServer;
using OnlineGameStore.DAL.Data;

namespace OnlineGameStore.DAL.Repositories.GameStore
{
    public class PublisherSqlServerRepository : SqlServerRepository<PublisherEntity>, IPublisherSqlServerRepository
    {
        public PublisherSqlServerRepository(StoreDbContext context, ILoggerFactory logger) : base(context, logger)
        {
        }

        public async Task<PublisherEntity> GetByNameAsync(string companyName,
            bool includeDeleted = false,
            params string[] includeProperties)
        {
            Expression<Func<PublisherEntity, bool>> predicate = p => p.CompanyName == companyName;
            var publisher = await IncludeProperties(includeDeleted, includeProperties).SingleOrDefaultAsync(predicate);

            return publisher;
        }
    }
}