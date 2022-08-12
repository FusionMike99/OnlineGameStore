using System;
using System.Linq.Expressions;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Models;

namespace OnlineGameStore.BLL.Pipelines.Filters.Games
{
    public class GamesByPublishersFilter : IFilter<SortFilterGameModel, Expression<Func<GameEntity, bool>>>
    {
        public Expression<Func<GameEntity, bool>> Execute(SortFilterGameModel input)
        {
            var selectedPublishers = input?.SelectedPublishers;
            
            Expression<Func<GameEntity, bool>> filterExpression = null;
            
            if (selectedPublishers != null)
            {
                filterExpression = g => selectedPublishers.Contains(g.PublisherName);
            }

            return filterExpression;
        }
    }
}