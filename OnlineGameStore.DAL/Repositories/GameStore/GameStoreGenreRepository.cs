using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Repositories.GameStore;
using OnlineGameStore.DAL.Data;

namespace OnlineGameStore.DAL.Repositories.GameStore
{
    public class GameStoreGenreRepository : GameStoreGenericRepository<GenreEntity>, IGameStoreGenreRepository
    {
        public GameStoreGenreRepository(StoreDbContext context, ILoggerFactory logger) : base(context, logger)
        {
        }

        public override async Task DeleteAsync(GenreEntity genre)
        {
            await base.DeleteAsync(genre);
            genre.SubGenres.ToList().ForEach(g => g.ParentId = g.Parent.ParentId);
            await Context.SaveChangesAsync();
        }

        public async Task<GenreEntity> GetByNameAsync(string name,
            bool includeDeleted = false,
            params string[] includeProperties)
        {
            Expression<Func<GenreEntity, bool>> predicate = g => g.Name == name;

            return await GetSingle(predicate, includeDeleted, includeProperties);
        }

        public async Task<IEnumerable<GenreEntity>> GetParentGenresAsync(bool includeDeleted = false,
            params string[] includeProperties)
        {
            Expression<Func<GenreEntity, bool>> predicate = g => !g.ParentId.HasValue;

            return await GetMany(predicate, includeDeleted: includeDeleted, includeProperties: includeProperties);
        }

        public async Task<IEnumerable<GenreEntity>> GetWithoutGenreAsync(Guid id,
            bool includeDeleted = false,
            params string[] includeProperties)
        {
            Expression<Func<GenreEntity, bool>> predicate = g => g.Id != id && g.ParentId != id;

            return await GetMany(predicate, includeDeleted: includeDeleted, includeProperties: includeProperties);
        }

        public async Task<IEnumerable<string>> GetIdsByNamesAsync(IEnumerable<string> genresNames)
        {
            var genres = await GetMany(g => genresNames.Contains(g.Name));
            var genreIds = genres.Select(g => g.Id.ToString());

            return genreIds;
        }
    }
}