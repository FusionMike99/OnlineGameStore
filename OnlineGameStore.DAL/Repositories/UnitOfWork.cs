using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.DAL.Data;

namespace OnlineGameStore.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _context;
        private GenericRepository<Comment, int> _commentRepository;

        private GenericRepository<Game, int> _gameRepository;
        private GenericRepository<Genre, int> _genreRepository;
        private GenericRepository<Order, int> _orderRepository;
        private GenericRepository<Publisher, int> _publisherRepository;
        private GenericRepository<PlatformType, int> _typeRepository;

        public UnitOfWork(StoreDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<Game, int> Games =>
            _gameRepository ??= new GenericRepository<Game, int>(_context);

        public IGenericRepository<Comment, int> Comments =>
            _commentRepository ??= new GenericRepository<Comment, int>(_context);

        public IGenericRepository<Genre, int> Genres =>
            _genreRepository ??= new GenericRepository<Genre, int>(_context);

        public IGenericRepository<PlatformType, int> PlatformTypes =>
            _typeRepository ??= new GenericRepository<PlatformType, int>(_context);

        public IGenericRepository<Publisher, int> Publishers =>
            _publisherRepository ??= new GenericRepository<Publisher, int>(_context);

        public IGenericRepository<Order, int> Orders =>
            _orderRepository ??= new GenericRepository<Order, int>(_context);

        public int Commit()
        {
            return _context.SaveChanges();
        }
    }
}