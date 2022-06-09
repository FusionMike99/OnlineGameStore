using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Enums;
using OnlineGameStore.BLL.Models;
using OnlineGameStore.MVC.Infrastructure;
using OnlineGameStore.MVC.Models;

namespace OnlineGameStore.MVC.ModelBuilders
{
    public class SortFilterGameViewModelBuilder
    {
        private readonly SortFilterGameViewModel _model;

        public SortFilterGameViewModelBuilder(SortFilterGameModel sortFilterGameModel,
            IEnumerable<Genre> genres,
            IEnumerable<PlatformType> platformTypes,
            IEnumerable<Publisher> publishers)
        {
            _model = new SortFilterGameViewModel();

            SetGenres(genres, sortFilterGameModel.SelectedGenres);
            SetPlatformTypes(platformTypes, sortFilterGameModel.SelectedPlatformTypes);
            SetPublishers(publishers, sortFilterGameModel.SelectedPublishers);
            
            SetSorting(sortFilterGameModel.GameSortState);
            SetDatePublished(sortFilterGameModel.DatePublishedPeriod);
            
            SetPriceRange(sortFilterGameModel.PriceRange);
            SetName(sortFilterGameModel.GameName);
        }

        public static implicit operator SortFilterGameViewModel(SortFilterGameViewModelBuilder builder) =>
            builder._model;

        private void SetGenres(IEnumerable<Genre> genres, List<int> selectedGenres)
        {
            _model.Genres = new SelectList(genres,
                nameof(Genre.Id),
                nameof(Genre.Name));
            
            if (selectedGenres?.Any() == true)
            {
                SetUpSelectList(_model.Genres, selectedGenres);
            }

            _model.SelectedGenres = selectedGenres;
        }
        
        private void SetPlatformTypes(IEnumerable<PlatformType> platformTypes, List<int> selectedPlatformTypes)
        {
            _model.PlatformTypes = new SelectList(platformTypes,
                nameof(PlatformType.Id),
                nameof(PlatformType.Type));
            
            if (selectedPlatformTypes?.Any() == true)
            {
                SetUpSelectList(_model.PlatformTypes, selectedPlatformTypes);
            }

            _model.SelectedPlatformTypes = selectedPlatformTypes;
        }
        
        private void SetPublishers(IEnumerable<Publisher> publishers, List<int> selectedPublishers)
        {
            _model.Publishers = new SelectList(publishers,
                nameof(Publisher.Id),
                nameof(Publisher.CompanyName));
            
            if (selectedPublishers?.Any() == true)
            {
                SetUpSelectList(_model.Publishers, selectedPublishers);
            }

            _model.SelectedPublishers = selectedPublishers;
        }
        
        private void SetSorting(GameSortState sortState)
        {
            _model.GameSortState = sortState;

            _model.SortParameters = sortState.ToSelectList();
        }
        
        private void SetDatePublished(DatePublishedPeriod publishedPeriod)
        {
            _model.DatePublishedPeriod = publishedPeriod;

            _model.DatePublishedParameters = publishedPeriod.ToSelectList();
        }

        private void SetPriceRange(PriceRangeModel priceRange)
        {
            _model.PriceRange = new PriceRangeViewModel
            {
                Min = priceRange?.Min,
                Max = priceRange?.Max
            };
        }

        private void SetName(string name) => _model.GameName = name;

        private static void SetUpSelectList(SelectList items, ICollection<int> selectedItems)
        {
            foreach (var item in items)
            {
                var parseResult = int.TryParse(item.Value, out var intValue);

                if (!parseResult)
                {
                    continue;
                }
                    
                var isItemContained = selectedItems.Contains(intValue);
                        
                if (isItemContained)
                {
                    item.Selected = true;
                }
            }
        }
    }
}