using System;
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
    public class GenreSqlServerRepository : SqlServerRepository<GenreEntity>, IGenreSqlServerRepository
    {
        public GenreSqlServerRepository(StoreDbContext context, ILoggerFactory logger) : base(context, logger)
        {
        }

        public override async Task DeleteAsync(GenreEntity genre)
        {
            await base.DeleteAsync(genre);
            genre.SubGenres.ToList().ForEach(g => g.ParentId = g.Parent.ParentId);
            await Context.SaveChangesAsync();
        }

        public override async Task<GenreEntity> GetByIdAsync(Guid id)
        {
            var genre = await Query.IncludeForGenres().FirstOrDefaultAsync(g => g.Id == id);

            return genre;
        }

        public async Task<GenreEntity> GetByNameAsync(string name)
        {
            var genre = await Query.FirstOrDefaultAsync(g => g.Name == name);

            return genre;
        }

        public async Task<GenreEntity> GetByNameIncludeDeletedAsync(string name)
        {
            var genre = await Query.IncludeDeleted().FirstOrDefaultAsync(g => g.Name == name);

            return genre;
        }

        public async Task<IEnumerable<GenreEntity>> GetParentGenresAsync()
        {
            var genres = await Query.IncludeForGenres()
                .Where(g => !g.ParentId.HasValue).ToListAsync();

            return genres;
        }

        public async Task<IEnumerable<GenreEntity>> GetWithoutGenreAsync(Guid id)
        {
            var genres = await Query.Where(g => g.Id != id && g.ParentId != id).ToListAsync();

            return genres;
        }

        public async Task<IEnumerable<string>> GetIdsByNamesAsync(IEnumerable<string> genresNames)
        {
            var genreIds = await Query.Where(g => genresNames.Contains(g.Name))
                .Select(g => g.Id.ToString()).ToListAsync();

            return genreIds;
        }
    }
}