using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Repositories.GameStore;
using OnlineGameStore.DAL.Data;

namespace OnlineGameStore.DAL.Repositories.GameStore
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _context;
        private GameStoreGenericRepository<Comment> _commentRepository;
        private GameStoreGenericRepository<Game> _gameRepository;
        private GameStoreGenericRepository<Genre> _genreRepository;
        private GameStoreGenericRepository<Order> _orderRepository;
        private GameStoreGenericRepository<Publisher> _publisherRepository;
        private GameStoreGenericRepository<PlatformType> _typeRepository;

        public UnitOfWork(StoreDbContext context)
        {
            _context = context;
        }

        public IGameStoreGenericRepository<Game> Games =>
            _gameRepository ??= new GameStoreGenericRepository<Game>(_context);

        public IGameStoreGenericRepository<Comment> Comments =>
            _commentRepository ??= new GameStoreGenericRepository<Comment>(_context);

        public IGameStoreGenericRepository<Genre> Genres =>
            _genreRepository ??= new GameStoreGenericRepository<Genre>(_context);

        public IGameStoreGenericRepository<PlatformType> PlatformTypes =>
            _typeRepository ??= new GameStoreGenericRepository<PlatformType>(_context);

        public IGameStoreGenericRepository<Publisher> Publishers =>
            _publisherRepository ??= new GameStoreGenericRepository<Publisher>(_context);

        public IGameStoreGenericRepository<Order> Orders =>
            _orderRepository ??= new GameStoreGenericRepository<Order>(_context);

        public int Commit()
        {
            return _context.SaveChanges();
        }
    }
}