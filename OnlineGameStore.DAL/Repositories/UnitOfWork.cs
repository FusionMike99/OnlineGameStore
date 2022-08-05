using System;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.DAL.Data;

namespace OnlineGameStore.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _context;
        private GenericRepository<Comment, Guid> _commentRepository;
        private GenericRepository<Game, Guid> _gameRepository;
        private GenericRepository<Genre, Guid> _genreRepository;
        private GenericRepository<Order, Guid> _orderRepository;
        private GenericRepository<Publisher, Guid> _publisherRepository;
        private GenericRepository<PlatformType, Guid> _typeRepository;

        public UnitOfWork(StoreDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<Game, Guid> Games =>
            _gameRepository ??= new GenericRepository<Game, Guid>(_context);

        public IGenericRepository<Comment, Guid> Comments =>
            _commentRepository ??= new GenericRepository<Comment, Guid>(_context);

        public IGenericRepository<Genre, Guid> Genres =>
            _genreRepository ??= new GenericRepository<Genre, Guid>(_context);

        public IGenericRepository<PlatformType, Guid> PlatformTypes =>
            _typeRepository ??= new GenericRepository<PlatformType, Guid>(_context);

        public IGenericRepository<Publisher, Guid> Publishers =>
            _publisherRepository ??= new GenericRepository<Publisher, Guid>(_context);

        public IGenericRepository<Order, Guid> Orders =>
            _orderRepository ??= new GenericRepository<Order, Guid>(_context);

        public int Commit()
        {
            return _context.SaveChanges();
        }
    }
}