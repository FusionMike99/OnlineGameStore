using System.Linq;
using Microsoft.EntityFrameworkCore;
using OnlineGameStore.DAL.Entities;

namespace OnlineGameStore.DAL.Repositories.SqlServer.Extensions
{
    internal static class SoftDeleteExtensions
    {
        internal static IQueryable<TEntity> IncludeDeleted<TEntity>(this IQueryable<TEntity> query)
            where TEntity : BaseEntity
        {
            query = query.IgnoreQueryFilters();

            return query;
        }
    }
}