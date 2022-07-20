using System;
using System.Linq.Expressions;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Models;

namespace OnlineGameStore.BLL.Pipelines.Filters.Games
{
    public class GamesByNameFilter : IFilter<SortFilterGameModel, Expression<Func<Game, bool>>>
    {
        public Expression<Func<Game, bool>> Execute(SortFilterGameModel input)
        {
            var gameName = input?.GameName;
            
            Expression<Func<Game, bool>> filterExpression = null;
            
            if (!string.IsNullOrWhiteSpace(gameName))
            {
                filterExpression = g => g.Name.ToLower().Contains(gameName.ToLower());
            }

            return filterExpression;
        }
    }
}