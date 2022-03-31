namespace OnlineGameStore.BLL.Entities
{
    public interface IBaseEntity<TKey>
    {
        public TKey Id { get; set; }
    }
}
