using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Repositories.GameStore;
using OnlineGameStore.DAL.Data;

namespace OnlineGameStore.DAL.Repositories.GameStore
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _context;
        private GenericRepository<Comment> _commentRepository;
        private GenericRepository<Game> _gameRepository;
        private GenericRepository<Genre> _genreRepository;
        private GenericRepository<Order> _orderRepository;
        private GenericRepository<Publisher> _publisherRepository;
        private GenericRepository<PlatformType> _typeRepository;

        public UnitOfWork(StoreDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<Game> Games =>
            _gameRepository ??= new GenericRepository<Game>(_context);

        public IGenericRepository<Comment> Comments =>
            _commentRepository ??= new GenericRepository<Comment>(_context);

        public IGenericRepository<Genre> Genres =>
            _genreRepository ??= new GenericRepository<Genre>(_context);

        public IGenericRepository<PlatformType> PlatformTypes =>
            _typeRepository ??= new GenericRepository<PlatformType>(_context);

        public IGenericRepository<Publisher> Publishers =>
            _publisherRepository ??= new GenericRepository<Publisher>(_context);

        public IGenericRepository<Order> Orders =>
            _orderRepository ??= new GenericRepository<Order>(_context);

        public int Commit()
        {
            return _context.SaveChanges();
        }
    }
}