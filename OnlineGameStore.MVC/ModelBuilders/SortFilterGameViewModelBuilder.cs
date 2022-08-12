using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Enums;
using OnlineGameStore.BLL.Models;
using OnlineGameStore.BLL.Services.Contracts;
using OnlineGameStore.MVC.Infrastructure;
using OnlineGameStore.MVC.Models;

namespace OnlineGameStore.MVC.ModelBuilders
{
    public class SortFilterGameViewModelBuilder
    {
        private SortFilterGameViewModel _model;

        private SortFilterGameViewModelBuilder()
        {
        }

        public static async Task<SortFilterGameViewModelBuilder> Create(SortFilterGameModel sortFilterGameModel,
            IGenreService genreService,
            IPlatformTypeService platformTypeService,
            IPublisherService publisherService)
        {
            var modelBuilder = new SortFilterGameViewModelBuilder
            {
                _model = new SortFilterGameViewModel()
            };

            await modelBuilder.SetGenres(genreService, sortFilterGameModel.SelectedGenres);
            await modelBuilder.SetPlatformTypes(platformTypeService, sortFilterGameModel.SelectedPlatformTypes);
            await modelBuilder.SetPublishers(publisherService, sortFilterGameModel.SelectedPublishers);
            modelBuilder.SetSorting(sortFilterGameModel.GameSortState);
            modelBuilder.SetDatePublished(sortFilterGameModel.DatePublishedPeriod);
            modelBuilder.SetPriceRange(sortFilterGameModel.PriceRange);
            modelBuilder.SetName(sortFilterGameModel.GameName);

            return modelBuilder;
        }

        public static implicit operator SortFilterGameViewModel(SortFilterGameViewModelBuilder builder) =>
            builder._model;

        private async Task SetGenres(IGenreService genreService, ICollection<string> selectedGenres)
        {
            var genres = (await genreService.GetAllGenresAsync()).ToList();
            
            _model.Genres = new SelectList(genres,
                nameof(GenreEntity.Name),
                nameof(GenreEntity.Name));

            var selectedGenresNames = new List<string>();
            
            if (selectedGenres?.Any() == true)
            {
                selectedGenresNames = genres
                    .Where(g => selectedGenres.Contains(g.Id.ToString()))
                    .Select(g => g.Name).ToList();
                
                SetUpSelectList(_model.Genres, selectedGenresNames);
            }

            _model.SelectedGenres = selectedGenresNames;
        }
        
        private async Task SetPlatformTypes(IPlatformTypeService platformTypeService,
            ICollection<string> selectedPlatformTypes)
        {
            var platformTypes = (await platformTypeService.GetAllPlatformTypesAsync()).ToList();
            
            _model.PlatformTypes = new SelectList(platformTypes,
                nameof(PlatformTypeEntity.Type),
                nameof(PlatformTypeEntity.Type));
            
            var selectedPlatformTypesNames = new List<string>();
            
            if (selectedPlatformTypes?.Any() == true)
            {
                selectedPlatformTypesNames = platformTypes
                    .Where(pt => selectedPlatformTypes.Contains(pt.Id.ToString()))
                    .Select(pt => pt.Type).ToList();
                
                SetUpSelectList(_model.PlatformTypes, selectedPlatformTypesNames);
            }

            _model.SelectedPlatformTypes = selectedPlatformTypesNames;
        }
        
        private async Task SetPublishers(IPublisherService publisherService, ICollection<string> selectedPublishers)
        {
            var publishers = (await publisherService.GetAllPublishersAsync()).ToList();
            
            _model.Publishers = new SelectList(publishers,
                nameof(PublisherEntity.CompanyName),
                nameof(PublisherEntity.CompanyName));
            
            var selectedPublishersNames = new List<string>();
            
            if (selectedPublishers?.Any() == true)
            {
                selectedPublishersNames = publishers
                    .Where(p => selectedPublishers.Contains(p.CompanyName))
                    .Select(p => p.CompanyName).ToList();
                
                SetUpSelectList(_model.Publishers, selectedPublishersNames);
            }

            _model.SelectedPublishers = selectedPublishersNames;
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

        private static void SetUpSelectList(SelectList items, ICollection<string> selectedItems)
        {
            foreach (var item in items)
            {
                var isItemContained = selectedItems.Contains(item.Value);
                        
                if (isItemContained)
                {
                    item.Selected = true;
                }
            }
        }
    }
}