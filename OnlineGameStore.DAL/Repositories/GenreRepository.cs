using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Models.General;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Repositories.MongoDb;
using OnlineGameStore.BLL.Repositories.SqlServer;

namespace OnlineGameStore.DAL.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly IGenreSqlServerRepository _genreSqlServerRepository;
        private readonly ICategoryMongoDbRepository _categoryMongoDbRepository;
        private readonly IMapper _mapper;

        public GenreRepository(IGenreSqlServerRepository genreSqlServerRepository,
            ICategoryMongoDbRepository categoryMongoDbRepository,
            IMapper mapper)
        {
            _genreSqlServerRepository = genreSqlServerRepository;
            _categoryMongoDbRepository = categoryMongoDbRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(GenreModel genreModel)
        {
            var genre = _mapper.Map<GenreEntity>(genreModel);
            var createdGenre = await _genreSqlServerRepository.CreateAsync(genre);
            genreModel.Id = createdGenre.Id;
        }

        public async Task UpdateAsync(GenreModel genreModel)
        {
            var genre = _mapper.Map<GenreEntity>(genreModel);
            await _genreSqlServerRepository.UpdateAsync(genre);
        }

        public async Task DeleteAsync(GenreModel genreModel)
        {
            var genre = _mapper.Map<GenreEntity>(genreModel);
            await _genreSqlServerRepository.DeleteAsync(genre);
        }

        public async Task<GenreModel> GetByIdAsync(Guid id, bool includeDeleted = false,
            params string[] includeProperties)
        {
            var genre = await _genreSqlServerRepository.GetByIdAsync(id, includeDeleted, includeProperties);
            var mappedGenre = _mapper.Map<GenreModel>(genre);

            return mappedGenre;
        }

        public async Task<GenreModel> GetByNameAsync(string name,
            bool includeDeleted = false,
            params string[] includeProperties)
        {
            var genre = await _genreSqlServerRepository.GetByNameAsync(name, includeDeleted, includeProperties);
            var mappedGenre = _mapper.Map<GenreModel>(genre);

            return mappedGenre;
        }

        public async Task<IEnumerable<string>> GetGenreIdsByNamesAsync(IEnumerable<string> names)
        {
            return await _genreSqlServerRepository.GetIdsByNamesAsync(names);
        }

        public async Task<IEnumerable<string>> GetCategoryIdsByNamesAsync(IEnumerable<string> names)
        {
            return await _categoryMongoDbRepository.GetIdsByNamesAsync(names);
        }

        public async Task<IEnumerable<GenreModel>> GetAllAsync(bool includeDeleted = false,
            params string[] includeProperties)
        {
            var genres = await _genreSqlServerRepository.GetAllAsync(includeDeleted, includeProperties);
            var mappedGenres = _mapper.Map<IEnumerable<GenreModel>>(genres);

            return mappedGenres;
        }

        public async Task<IEnumerable<GenreModel>> GetParentGenresAsync(bool includeDeleted = false,
            params string[] includeProperties)
        {
            var genres = await _genreSqlServerRepository.GetParentGenresAsync(includeDeleted, includeProperties);
            var mappedGenres = _mapper.Map<IEnumerable<GenreModel>>(genres);

            return mappedGenres;
        }

        public async Task<IEnumerable<GenreModel>> GetWithoutGenreAsync(Guid id,
            bool includeDeleted = false,
            params string[] includeProperties)
        {
            var genres = await _genreSqlServerRepository.GetWithoutGenreAsync(id, includeDeleted, includeProperties);
            var mappedGenres = _mapper.Map<IEnumerable<GenreModel>>(genres);

            return mappedGenres;
        }
    }
}