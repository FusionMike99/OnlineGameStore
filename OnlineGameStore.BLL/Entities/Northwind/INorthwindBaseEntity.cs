using MongoDB.Bson;

namespace OnlineGameStore.BLL.Entities.Northwind
{
    public interface INorthwindBaseEntity<TKey>
    {
        public TKey Id { get; set; }
    }
}