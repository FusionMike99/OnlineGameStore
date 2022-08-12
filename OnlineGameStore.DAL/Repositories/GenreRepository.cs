using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Models.General;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Repositories.GameStore;
using OnlineGameStore.BLL.Repositories.Northwind;

namespace OnlineGameStore.DAL.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly IGameStoreGenreRepository _genreRepository;
        private readonly INorthwindCategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public GenreRepository(IGameStoreGenreRepository genreRepository,
            INorthwindCategoryRepository categoryRepository,
            IMapper mapper)
        {
            _genreRepository = genreRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(GenreModel genreModel)
        {
            var genre = _mapper.Map<GenreEntity>(genreModel);
            var createdGenre = await _genreRepository.CreateAsync(genre);
            genreModel.Id = createdGenre.Id;
        }

        public async Task UpdateAsync(GenreModel genreModel)
        {
            var genre = _mapper.Map<GenreEntity>(genreModel);
            await _genreRepository.UpdateAsync(genre);
        }

        public async Task DeleteAsync(GenreModel genreModel)
        {
            var genre = _mapper.Map<GenreEntity>(genreModel);
            await _genreRepository.DeleteAsync(genre);
        }

        public async Task<GenreModel> GetByIdAsync(Guid id, bool includeDeleted = false,
            params string[] includeProperties)
        {
            var genre = await _genreRepository.GetByIdAsync(id, includeDeleted, includeProperties);
            var mappedGenre = _mapper.Map<GenreModel>(genre);

            return mappedGenre;
        }

        public async Task<GenreModel> GetByNameAsync(string name,
            bool includeDeleted = false,
            params string[] includeProperties)
        {
            var genre = await _genreRepository.GetByNameAsync(name, includeDeleted, includeProperties);
            var mappedGenre = _mapper.Map<GenreModel>(genre);

            return mappedGenre;
        }

        public async Task<IEnumerable<string>> GetGenreIdsByNamesAsync(IEnumerable<string> names)
        {
            return await _genreRepository.GetIdsByNamesAsync(names);
        }

        public async Task<IEnumerable<string>> GetCategoryIdsByNamesAsync(IEnumerable<string> names)
        {
            return await _categoryRepository.GetIdsByNamesAsync(names);
        }

        public async Task<IEnumerable<GenreModel>> GetAllAsync(bool includeDeleted = false,
            params string[] includeProperties)
        {
            var genres = await _genreRepository.GetAllAsync(includeDeleted, includeProperties);
            var mappedGenres = _mapper.Map<IEnumerable<GenreModel>>(genres);

            return mappedGenres;
        }

        public async Task<IEnumerable<GenreModel>> GetParentGenresAsync(bool includeDeleted = false,
            params string[] includeProperties)
        {
            var genres = await _genreRepository.GetParentGenresAsync(includeDeleted, includeProperties);
            var mappedGenres = _mapper.Map<IEnumerable<GenreModel>>(genres);

            return mappedGenres;
        }

        public async Task<IEnumerable<GenreModel>> GetWithoutGenreAsync(Guid id,
            bool includeDeleted = false,
            params string[] includeProperties)
        {
            var genres = await _genreRepository.GetWithoutGenreAsync(id, includeDeleted, includeProperties);
            var mappedGenres = _mapper.Map<IEnumerable<GenreModel>>(genres);

            return mappedGenres;
        }
    }
}