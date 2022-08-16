using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using OnlineGameStore.DAL.Abstractions.Interfaces;
using OnlineGameStore.DAL.Entities;
using OnlineGameStore.DAL.Repositories.MongoDb.Interfaces;
using OnlineGameStore.DAL.Repositories.SqlServer.Interfaces;
using OnlineGameStore.DomainModels.Models.General;

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

        public async Task<GenreModel> GetByIdAsync(Guid id)
        {
            var genre = await _genreSqlServerRepository.GetByIdAsync(id);
            var mappedGenre = _mapper.Map<GenreModel>(genre);

            return mappedGenre;
        }

        public async Task<GenreModel> GetByNameAsync(string name)
        {
            var genre = await _genreSqlServerRepository.GetByNameAsync(name);
            var mappedGenre = _mapper.Map<GenreModel>(genre);

            return mappedGenre;
        }

        public async Task<GenreModel> GetByNameIncludeDeletedAsync(string name)
        {
            var genre = await _genreSqlServerRepository.GetByNameIncludeDeletedAsync(name);
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

        public async Task<IEnumerable<GenreModel>> GetAllAsync()
        {
            var genres = await _genreSqlServerRepository.GetAllAsync();
            var mappedGenres = _mapper.Map<IEnumerable<GenreModel>>(genres);

            return mappedGenres;
        }

        public async Task<IEnumerable<GenreModel>> GetParentGenresAsync()
        {
            var genres = await _genreSqlServerRepository.GetParentGenresAsync();
            var mappedGenres = _mapper.Map<IEnumerable<GenreModel>>(genres);

            return mappedGenres;
        }

        public async Task<IEnumerable<GenreModel>> GetWithoutGenreAsync(Guid id)
        {
            var genres = await _genreSqlServerRepository.GetWithoutGenreAsync(id);
            var mappedGenres = _mapper.Map<IEnumerable<GenreModel>>(genres);

            return mappedGenres;
        }
    }
}