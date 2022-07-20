using OnlineGameStore.BLL.Enums;
using OnlineGameStore.BLL.Models;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Services.Contracts;

namespace OnlineGameStore.BLL.Services
{
    public class NorthwindLogService : INorthwindLogService
    {
        private readonly INorthwindUnitOfWork _northwindUnitOfWork;

        public NorthwindLogService(INorthwindUnitOfWork northwindUnitOfWork)
        {
            _northwindUnitOfWork = northwindUnitOfWork;
        }

        public void LogCreating<T>(T entity)
        {
            var logModel = new LogModel(ActionTypes.Create, entity.GetType(), newObject: entity);

            _northwindUnitOfWork.Logs.Create(logModel);
        }

        public void LogUpdating<T>(T oldEntity, T newEntity)
        {
            var logModel = new LogModel(ActionTypes.Update, oldEntity.GetType(), oldEntity, newEntity);

            _northwindUnitOfWork.Logs.Create(logModel);
        }

        public void LogDeleting<T>(T entity)
        {
            var logModel = new LogModel(ActionTypes.Delete, entity.GetType(), oldObject: entity);

            _northwindUnitOfWork.Logs.Create(logModel);
        }
    }
}