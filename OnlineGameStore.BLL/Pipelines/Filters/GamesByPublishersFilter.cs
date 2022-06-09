using System;
using System.Linq;
using System.Linq.Expressions;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Models;

namespace OnlineGameStore.BLL.Pipelines.Filters
{
    public class GamesByPublishersFilter : IFilter<SortFilterGameModel, Expression<Func<Game, bool>>>
    {
        public Expression<Func<Game, bool>> Execute(SortFilterGameModel input)
        {
            var selectedPublishers = input?.SelectedPublishers;
            
            Expression<Func<Game, bool>> filterExpression = null;
            
            if (selectedPublishers?.Any() == true)
            {
                filterExpression = g => g.PublisherId.HasValue && 
                                        selectedPublishers.Any(id => id == (int)g.PublisherId);
            }

            return filterExpression;
        }
    }
}