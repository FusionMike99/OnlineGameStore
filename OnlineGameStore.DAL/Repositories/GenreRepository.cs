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
            var genre = _mapper.Map<Genre>(genreModel);

            var createdGenre = await _genreRepository.Create(genre);

            genreModel.Id = createdGenre.Id.ToString();
        }

        public async Task UpdateAsync(GenreModel genreModel)
        {
            var genre = _mapper.Map<Genre>(genreModel);

            await _genreRepository.Update(genre);
        }

        public async Task DeleteAsync(GenreModel genreModel)
        {
            var genre = _mapper.Map<Genre>(genreModel);

            await _genreRepository.Delete(genre);
        }

        public async Task<GenreModel> GetByIdAsync(string id,
            bool includeDeleted = false,
            params string[] includeProperties)
        {
            var isParsed = Guid.TryParse(id, out var genreGuid);

            if (!isParsed)
            {
                return null;
            }
            
            var genre = await _genreRepository.GetById(genreGuid, includeDeleted, includeProperties);
            var mappedGenre = _mapper.Map<GenreModel>(genre);

            return mappedGenre;
        }

        public async Task<GenreModel> GetByNameAsync(string name,
            bool includeDeleted = false,
            params string[] includeProperties)
        {
            var genre = await _genreRepository.GetByName(name, includeDeleted, includeProperties);
            var mappedGenre = _mapper.Map<GenreModel>(genre);

            return mappedGenre;
        }

        public async Task<IEnumerable<string>> GetGenreIdsByNamesAsync(IEnumerable<string> names)
        {
            return await _genreRepository.GetIdsByNames(names);
        }

        public async Task<IEnumerable<string>> GetCategoryIdsByNamesAsync(IEnumerable<string> names)
        {
            return await _categoryRepository.GetIdsByNames(names);
        }

        public async Task<IEnumerable<GenreModel>> GetAllAsync(bool includeDeleted = false,
            params string[] includeProperties)
        {
            var genres = await _genreRepository.GetAll(includeDeleted, includeProperties);
            var mappedGenres = _mapper.Map<IEnumerable<GenreModel>>(genres);

            return mappedGenres;
        }

        public async Task<IEnumerable<GenreModel>> GetParentGenres(bool includeDeleted = false,
            params string[] includeProperties)
        {
            var genres = await _genreRepository.GetParentGenres(includeDeleted, includeProperties);
            var mappedGenres = _mapper.Map<IEnumerable<GenreModel>>(genres);

            return mappedGenres;
        }

        public async Task<IEnumerable<GenreModel>> GetWithoutGenre(string id,
            bool includeDeleted = false,
            params string[] includeProperties)
        {
            var isParsed = Guid.TryParse(id, out var genreGuid);

            if (!isParsed)
            {
                return null;
            }
            
            var genres = await _genreRepository.GetWithoutGenre(genreGuid, includeDeleted, includeProperties);
            var mappedGenres = _mapper.Map<IEnumerable<GenreModel>>(genres);

            return mappedGenres;
        }
    }
}