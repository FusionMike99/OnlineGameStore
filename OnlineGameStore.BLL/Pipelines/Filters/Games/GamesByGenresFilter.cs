﻿using System;
using System.Linq;
using System.Linq.Expressions;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Models;

namespace OnlineGameStore.BLL.Pipelines.Filters.Games
{
    public class GamesByGenresFilter : IFilter<SortFilterGameModel, Expression<Func<GameEntity, bool>>>
    {
        public Expression<Func<GameEntity, bool>> Execute(SortFilterGameModel input)
        {
            var selectedGenres = input?.SelectedGenres;
            
            Expression<Func<GameEntity, bool>> filterExpression = null;
            
            if (selectedGenres != null)
            {
                filterExpression = g => g.GameGenres.Any(gg =>
                    selectedGenres.Any(id => id == gg.GenreId.ToString()));
            }

            return filterExpression;
        }
    }
}