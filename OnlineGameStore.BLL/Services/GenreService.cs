using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Models.General;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Services.Contracts;

namespace OnlineGameStore.BLL.Services
{
    public class GenreService : IGenreService
    {
        private readonly ILogger<GenreService> _logger;
        private readonly IGenreRepository _genreRepository;

        public GenreService(ILogger<GenreService> logger,
            IGenreRepository genreRepository)
        {
            _logger = logger;
            _genreRepository = genreRepository;
        }

        public async Task<GenreModel> CreateGenre(GenreModel genre)
        {
            await _genreRepository.CreateAsync(genre);

            _logger.LogDebug($@"Class: {nameof(GenreService)}; Method: {nameof(CreateGenre)}.
                    Creating genre with id {genre.Id} successfully", genre);

            return genre;
        }

        public async Task DeleteGenre(string genreId)
        {
            var genre = await _genreRepository.GetByIdAsync(genreId);

            if (genre == null)
            {
                var exception = new InvalidOperationException("Genre has not been found");

                _logger.LogError(exception, $@"Class: {nameof(GenreService)}; Method: {nameof(DeleteGenre)}.
                    Deleting genre with id {genreId} unsuccessfully", genreId);

                throw exception;
            }

            await _genreRepository.DeleteAsync(genre);

            _logger.LogDebug($@"Class: {nameof(GenreService)}; Method: {nameof(DeleteGenre)}.
                    Deleting genre with id {genreId} successfully", genre);
        }

        public async Task<GenreModel> EditGenre(GenreModel genre)
        {
            await _genreRepository.UpdateAsync(genre);

            _logger.LogDebug($@"Class: {nameof(GenreService)}; Method: {nameof(EditGenre)}.
                    Editing genre with id {genre.Id} successfully", genre);

            return genre;
        }

        public async Task<IEnumerable<GenreModel>> GetAllGenres()
        {
            var genres = await _genreRepository.GetAllAsync();

            _logger.LogDebug($@"Class: {nameof(GenreService)}; Method: {nameof(GetAllGenres)}.
                    Receiving genres successfully", genres);

            return genres;
        }

        public async Task<IEnumerable<GenreModel>> GetAllParentGenres()
        {
            var genres = await _genreRepository
                .GetParentGenres(includeProperties: $"{nameof(Genre.SubGenres)}");

            return genres;
        }

        public async Task<IEnumerable<GenreModel>> GetAllWithoutGenre(string genreId)
        {
            var genres = await _genreRepository.GetWithoutGenre(genreId);

            return genres;
        }

        public async Task<IEnumerable<string>> GetGenresIdsByNames(params string[] genresNames)
        {
            var genresIds = await _genreRepository.GetGenreIdsByNamesAsync(genresNames);

            return genresIds;
        }

        public async Task<IEnumerable<string>> GetCategoriesIdsByNames(IEnumerable<string> categoriesNames)
        {
            var categoryIds = await _genreRepository.GetCategoryIdsByNamesAsync(categoriesNames);

            return categoryIds;
        }

        public async Task<GenreModel> GetGenreById(string genreId)
        {
            var genre = await _genreRepository.GetByIdAsync(genreId,
                includeProperties: $"{nameof(Genre.SubGenres)}");

            return genre;
        }

        public async Task<bool> CheckNameForUnique(string genreId, string name)
        {
            var genre = await _genreRepository.GetByNameAsync(name, includeDeleted: true);

            return genre != null && genre.Id != genreId;
        }
    }
}