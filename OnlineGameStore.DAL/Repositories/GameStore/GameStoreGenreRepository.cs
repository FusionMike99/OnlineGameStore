using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Repositories.GameStore;
using OnlineGameStore.DAL.Data;

namespace OnlineGameStore.DAL.Repositories.GameStore
{
    public class GameStoreGenreRepository : GameStoreGenericRepository<Genre>, IGameStoreGenreRepository
    {
        public GameStoreGenreRepository(StoreDbContext context) : base(context)
        {
        }

        public override async Task Delete(Genre genre)
        {
            await base.Delete(genre);
            
            genre.SubGenres.ToList().ForEach(g => g.ParentId = g.Parent.ParentId);

            await Context.SaveChangesAsync();
        }

        public async Task<Genre> GetByName(string name,
            bool includeDeleted = false,
            params string[] includeProperties)
        {
            Expression<Func<Genre, bool>> predicate = g => g.Name == name;

            return await GetSingle(predicate, includeDeleted, includeProperties);
        }

        public async Task<IEnumerable<Genre>> GetParentGenres(bool includeDeleted = false,
            params string[] includeProperties)
        {
            Expression<Func<Genre, bool>> predicate = g => !g.ParentId.HasValue;

            return await GetMany(predicate,
                includeDeleted: includeDeleted,
                includeProperties: includeProperties);
        }

        public async Task<IEnumerable<Genre>> GetWithoutGenre(Guid id,
            bool includeDeleted = false,
            params string[] includeProperties)
        {
            Expression<Func<Genre, bool>> predicate = g => g.Id != id && g.ParentId != id;

            return await GetMany(predicate,
                includeDeleted: includeDeleted,
                includeProperties: includeProperties);
        }

        public async Task<IEnumerable<string>> GetIdsByNames(IEnumerable<string> genresNames)
        {
            var genres = await GetMany(g => genresNames.Contains(g.Name));

            var genreIds = genres.Select(g => g.Id.ToString());

            return genreIds;
        }
    }
}