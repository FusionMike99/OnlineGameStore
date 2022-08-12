using System;
using System.Linq.Expressions;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Models;

namespace OnlineGameStore.BLL.Pipelines.Filters.Games
{
    public class GamesByNameFilter : IFilter<SortFilterGameModel, Expression<Func<GameEntity, bool>>>
    {
        public Expression<Func<GameEntity, bool>> Execute(SortFilterGameModel input)
        {
            var gameName = input?.GameName;
            
            Expression<Func<GameEntity, bool>> filterExpression = null;
            
            if (!string.IsNullOrWhiteSpace(gameName))
            {
                filterExpression = g => g.Name.ToLower().Contains(gameName.ToLower());
            }

            return filterExpression;
        }
    }
}