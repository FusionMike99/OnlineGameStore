namespace OnlineGameStore.BLL.Services.Contracts
{
    public interface INorthwindLogService
    {
        void LogCreating<T>(T entity);

        void LogUpdating<T>(T oldEntity, T newEntity);
        
        void LogDeleting<T>(T entity);
    }
}