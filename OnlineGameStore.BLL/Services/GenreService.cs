using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Services.Contracts;

namespace OnlineGameStore.BLL.Services
{
    public class GenreService : IGenreService
    {
        private readonly ILogger<GenreService> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public GenreService(IUnitOfWork unitOfWork, ILogger<GenreService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public Genre CreateGenre(Genre genre)
        {
            var createdGenre = _unitOfWork.Genres.Create(genre);
            _unitOfWork.Commit();

            _logger.LogDebug($@"Class: {nameof(GenreService)}; Method: {nameof(CreateGenre)}.
                    Creating genre with id {createdGenre.Id} successfully", createdGenre);

            return createdGenre;
        }

        public void DeleteGenre(int genreId)
        {
            var genre = _unitOfWork.Genres.GetSingle(g => g.Id == genreId,
                includeDeleteEntities: false,
                $"{nameof(Genre.SubGenres)}");

            if (genre == null)
            {
                var exception = new InvalidOperationException("Genre has not been found");

                _logger.LogError(exception, $@"Class: {nameof(GenreService)}; Method: {nameof(DeleteGenre)}.
                    Deleting genre with id {genreId} unsuccessfully", genreId);

                throw exception;
            }

            _unitOfWork.Genres.Delete(genre);

            genre.SubGenres.ToList()
                .ForEach(g => g.ParentId = g.Parent.ParentId);
            
            _unitOfWork.Commit();

            _logger.LogDebug($@"Class: {nameof(GenreService)}; Method: {nameof(DeleteGenre)}.
                    Deleting genre with id {genreId} successfully", genre);
        }

        public Genre EditGenre(Genre genre)
        {
            var editedGenre = _unitOfWork.Genres.Update(genre);
            _unitOfWork.Commit();

            _logger.LogDebug($@"Class: {nameof(GenreService)}; Method: {nameof(EditGenre)}.
                    Editing genre with id {editedGenre.Id} successfully", editedGenre);

            return editedGenre;
        }

        public IEnumerable<Genre> GetAllGenres()
        {
            var genres = _unitOfWork.Genres.GetMany(null,
                    false,
                    null,
                    null,
                    null,
                    $"{nameof(Genre.GameGenres)}.{nameof(GameGenre.Game)}",
                    $"{nameof(Genre.Parent)}",
                    $"{nameof(Genre.SubGenres)}");

            _logger.LogDebug($@"Class: {nameof(GenreService)}; Method: {nameof(GetAllGenres)}.
                    Receiving genres successfully", genres);

            return genres;
        }

        public IEnumerable<Genre> GetAllParentGenres()
        {
            var genres = _unitOfWork.Genres.GetMany(g => !g.ParentId.HasValue,
                    false,
                    null,
                    null,
                    null,
                    $"{nameof(Genre.GameGenres)}.{nameof(GameGenre.Game)}",
                    $"{nameof(Genre.Parent)}",
                    $"{nameof(Genre.SubGenres)}");

            _logger.LogDebug($@"Class: {nameof(GenreService)}; Method: {nameof(GetAllParentGenres)}.
                    Receiving parent genres successfully", genres);

            return genres;
        }

        public IEnumerable<Genre> GetAllWithoutGenre(int genreId)
        {
            var genres = _unitOfWork.Genres.GetMany(g => g.Id != genreId && g.ParentId != genreId,
                    false,
                    null,
                    null,
                    null,
                    $"{nameof(Genre.GameGenres)}.{nameof(GameGenre.Game)}");

            _logger.LogDebug($@"Class: {nameof(GenreService)}; Method: {nameof(GetAllWithoutGenre)}.
                    Receiving genres without genre with id {genreId} successfully", genres);

            return genres;
        }

        public Genre GetGenreById(int genreId)
        {
            var genre = _unitOfWork.Genres.GetSingle(g => g.Id == genreId,
                    false,
                    $"{nameof(Genre.GameGenres)}.{nameof(GameGenre.Game)}",
                    $"{nameof(Genre.Parent)}",
                    $"{nameof(Genre.SubGenres)}");

            _logger.LogDebug($@"Class: {nameof(GenreService)}; Method: {nameof(GetGenreById)}.
                    Receiving genre with id {genreId} successfully", genre);

            return genre;
        }

        public bool CheckNameForUnique(int genreId, string name)
        {
            var genre = _unitOfWork.Genres.GetSingle(g => g.Name == name, true);

            return genre != null && genre.Id != genreId;
        }
    }
}