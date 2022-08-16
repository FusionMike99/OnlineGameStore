using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineGameStore.DAL.Data;
using OnlineGameStore.DAL.Entities;
using OnlineGameStore.DAL.Repositories.SqlServer.Extensions;
using OnlineGameStore.DAL.Repositories.SqlServer.Interfaces;

namespace OnlineGameStore.DAL.Repositories.SqlServer
{
    public class PublisherSqlServerRepository : SqlServerRepository<PublisherEntity>, IPublisherSqlServerRepository
    {
        public PublisherSqlServerRepository(StoreDbContext context, ILoggerFactory logger) : base(context, logger)
        {
        }

        public async Task<PublisherEntity> GetByNameAsync(string companyName)
        {
            var publisher = await Entities.FirstOrDefaultAsync(p => p.CompanyName == companyName);

            return publisher;
        }

        public async Task<PublisherEntity> GetByNameIncludeDeletedAsync(string companyName)
        {
            var publisher = await Entities.IncludeDeleted().FirstOrDefaultAsync(p => p.CompanyName == companyName);

            return publisher;
        }
    }
}