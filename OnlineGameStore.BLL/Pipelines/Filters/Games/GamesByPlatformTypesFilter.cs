using System;
using System.Linq;
using System.Linq.Expressions;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Models;

namespace OnlineGameStore.BLL.Pipelines.Filters.Games
{
    public class GamesByPlatformTypesFilter : IFilter<SortFilterGameModel, Expression<Func<GameEntity, bool>>>
    {
        public Expression<Func<GameEntity, bool>> Execute(SortFilterGameModel input)
        {
            var selectedPlatformTypes = input?.SelectedPlatformTypes;
            
            Expression<Func<GameEntity, bool>> filterExpression = null;
            
            if (selectedPlatformTypes != null)
            {
                filterExpression = g => g.GamePlatformTypes.Any(gp =>
                    selectedPlatformTypes.Any(id => id == gp.PlatformId.ToString()));
            }

            return filterExpression;
        }
    }
}