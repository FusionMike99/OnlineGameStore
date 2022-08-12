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

        public async Task<GenreModel> CreateGenreAsync(GenreModel genre)
        {
            await _genreRepository.CreateAsync(genre);

            return genre;
        }

        public async Task DeleteGenreAsync(Guid genreId)
        {
            var genre = await _genreRepository.GetByIdAsync(genreId);

            if (genre == null)
            {
                var exception = new InvalidOperationException("Genre has not been found");

                _logger.LogError(exception, @"Service: {Service}; Method: {Method}.
                    Deleting genre with id {GenreId} unsuccessfully", nameof(GenreService), nameof(DeleteGenreAsync), genreId);

                throw exception;
            }

            await _genreRepository.DeleteAsync(genre);
        }

        public async Task<GenreModel> EditGenreAsync(GenreModel genre)
        {
            await _genreRepository.UpdateAsync(genre);

            return genre;
        }

        public async Task<IEnumerable<GenreModel>> GetAllGenresAsync()
        {
            var genres = await _genreRepository.GetAllAsync();

            return genres;
        }

        public async Task<IEnumerable<GenreModel>> GetAllParentGenresAsync()
        {
            var genres = await _genreRepository
                .GetParentGenresAsync(includeProperties: $"{nameof(GenreEntity.SubGenres)}");

            return genres;
        }

        public async Task<IEnumerable<GenreModel>> GetAllWithoutGenreAsync(Guid genreId)
        {
            var genres = await _genreRepository.GetWithoutGenreAsync(genreId);

            return genres;
        }

        public async Task<IEnumerable<string>> GetGenresIdsByNamesAsync(params string[] genresNames)
        {
            var genresIds = await _genreRepository.GetGenreIdsByNamesAsync(genresNames);

            return genresIds;
        }

        public async Task<IEnumerable<string>> GetCategoriesIdsByNamesAsync(IEnumerable<string> categoriesNames)
        {
            var categoryIds = await _genreRepository.GetCategoryIdsByNamesAsync(categoriesNames);

            return categoryIds;
        }

        public async Task<GenreModel> GetGenreByIdAsync(Guid genreId)
        {
            var genre = await _genreRepository.GetByIdAsync(genreId,
                includeProperties: $"{nameof(GenreEntity.SubGenres)}");

            return genre;
        }

        public async Task<bool> CheckNameForUniqueAsync(Guid genreId, string name)
        {
            var genre = await _genreRepository.GetByNameAsync(name, includeDeleted: true);

            return genre != null && genre.Id != genreId;
        }
    }
}