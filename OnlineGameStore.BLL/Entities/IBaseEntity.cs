namespace OnlineGameStore.BLL.Entities
{
    public interface IBaseEntity<TKey> : ISoftDelete
    {
        public TKey Id { get; set; }
    }
}