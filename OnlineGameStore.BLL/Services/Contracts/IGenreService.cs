using OnlineGameStore.BLL.Entities;
using System.Collections.Generic;

namespace OnlineGameStore.BLL.Services.Contracts
{
    public interface IGenreService
    {
        Genre CreateGenre(Genre genre);

        Genre EditGenre(Genre genre);

        void DeleteGenre(int genreId);

        Genre GetGenreById(int genreId);

        IEnumerable<Genre> GetAllParentGenres();

        IEnumerable<Genre> GetAllWithoutGenre(int genreId);

        bool CheckNameForUniqueness(int genreId, string name);
    }
}
