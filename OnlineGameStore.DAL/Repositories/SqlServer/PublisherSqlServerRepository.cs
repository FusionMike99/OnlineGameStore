using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineGameStore.DAL.Data;
using OnlineGameStore.DAL.Entities;
using OnlineGameStore.DAL.Repositories.SqlServer.Interfaces;

namespace OnlineGameStore.DAL.Repositories.SqlServer
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