using System;
using System.Linq.Expressions;
using OnlineGameStore.DAL.Entities;
using OnlineGameStore.DomainModels.Enums;
using OnlineGameStore.DomainModels.Models;

namespace OnlineGameStore.DAL.Pipelines.Filters.Games
{
    public class GamesByDatePublishedFilter : IFilter<SortFilterGameModel, Expression<Func<GameEntity, bool>>>
    {
        public Expression<Func<GameEntity, bool>> Execute(SortFilterGameModel input)
        {
            var datePublishedPeriod = input?.DatePublishedPeriod;
            
            if (datePublishedPeriod == null || datePublishedPeriod == DatePublishedPeriod.None)
            {
                return null;
            }

            var minPublishedDate = datePublishedPeriod switch
            {
                DatePublishedPeriod.LastWeek => DateTime.UtcNow.AddDays(-7),
                DatePublishedPeriod.LastMonth => DateTime.UtcNow.AddMonths(-1),
                DatePublishedPeriod.LastYear => DateTime.UtcNow.AddYears(-1),
                DatePublishedPeriod.TwoYear => DateTime.UtcNow.AddYears(-2),
                DatePublishedPeriod.ThreeYear => DateTime.UtcNow.AddYears(-3),
                _ => throw new ArgumentOutOfRangeException(nameof(datePublishedPeriod), datePublishedPeriod, null)
            };

            Expression<Func<GameEntity, bool>> filterExpression = g => g.DatePublished >= minPublishedDate;

            return filterExpression;
        }
    }
}