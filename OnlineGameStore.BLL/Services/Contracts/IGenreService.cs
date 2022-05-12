using System.Collections.Generic;
using OnlineGameStore.BLL.Entities;

namespace OnlineGameStore.BLL.Services.Contracts
{
    public interface IGenreService
    {
        Genre CreateGenre(Genre genre);

        Genre EditGenre(Genre genre);

        void DeleteGenre(int genreId);

        Genre GetGenreById(int genreId);

        IEnumerable<Genre> GetAllGenres();
        
        IEnumerable<Genre> GetAllParentGenres();

        IEnumerable<Genre> GetAllWithoutGenre(int genreId);

        bool CheckNameForUnique(int genreId, string name);
    }
}