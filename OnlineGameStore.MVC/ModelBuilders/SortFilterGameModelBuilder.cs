﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Enums;
using OnlineGameStore.BLL.Models;
using OnlineGameStore.BLL.Services.Contracts;
using OnlineGameStore.MVC.Models;

namespace OnlineGameStore.MVC.ModelBuilders
{
    public class SortFilterGameModelBuilder
    {
        private SortFilterGameModel _model;

        private SortFilterGameModelBuilder()
        {
        }
        
        public static async Task<SortFilterGameModelBuilder> Create(SortFilterGameViewModel sortFilterGameViewModel,
            IGenreService genreService,
            IPlatformTypeService platformTypeService,
            IPublisherService publisherService)
        {
            var modelBuilder = new SortFilterGameModelBuilder
            {
                _model = new SortFilterGameModel()
            };

            var tasks = new[]
            {
                modelBuilder.SetGenres(genreService, sortFilterGameViewModel.SelectedGenres),
                modelBuilder.SetPlatformTypes(platformTypeService, sortFilterGameViewModel.SelectedPlatformTypes),
                modelBuilder.SetPublishers(publisherService, sortFilterGameViewModel.SelectedPublishers),
                Task.Run(() => modelBuilder.SetSorting(sortFilterGameViewModel.GameSortState)),
                Task.Run(() => modelBuilder.SetDatePublished(sortFilterGameViewModel.DatePublishedPeriod)),
                Task.Run(() => modelBuilder.SetPriceRange(sortFilterGameViewModel.PriceRange)),
                Task.Run(() => modelBuilder.SetName(sortFilterGameViewModel.GameName)),
            };

            await Task.WhenAll(tasks);

            return modelBuilder;
        }
        
        public static implicit operator SortFilterGameModel(SortFilterGameModelBuilder builder) =>
            builder._model;
        
        private async Task SetGenres(IGenreService genreService, ICollection<string> selectedGenres)
        {
            List<string> selectedGenresIds = null;
            List<string> selectedCategoriesIds = null;
            
            if (selectedGenres?.Any() == true)
            {
                var genresTask = genreService.GetGenresIdsByNames(selectedGenres.ToArray());
                var categoriesTask = genreService.GetCategoriesIdsByNames(selectedGenres);

                await Task.WhenAll(genresTask, categoriesTask);

                selectedGenresIds = (await genresTask).ToList();
                selectedCategoriesIds = (await categoriesTask).ToList();
            }

            _model.SelectedGenres = selectedGenresIds;
            _model.SelectedCategories = selectedCategoriesIds;
        }
        
        private async Task SetPlatformTypes(IPlatformTypeService platformTypeService, ICollection<string> selectedPlatformTypes)
        {
            List<string> selectedPlatformTypesIds = null;
            
            if (selectedPlatformTypes?.Any() == true)
            {
                selectedPlatformTypesIds = (await platformTypeService
                    .GetPlatformTypesIdsByNames(selectedPlatformTypes)).ToList();
            }

            _model.SelectedPlatformTypes = selectedPlatformTypesIds;
        }
        
        private async Task SetPublishers(IPublisherService publisherService, List<string> selectedPublishers)
        {
            List<string> selectedSuppliersIds = null;
            
            if (selectedPublishers?.Any() == true)
            {
                selectedSuppliersIds = (await publisherService.GetSuppliersIdsByNames(selectedPublishers)).ToList();
            }

            _model.SelectedPublishers = selectedPublishers;
            _model.SelectedSuppliers = selectedSuppliersIds;
        }
        
        private void SetSorting(GameSortState sortState) => _model.GameSortState = sortState;
        
        private void SetDatePublished(DatePublishedPeriod publishedPeriod) => 
            _model.DatePublishedPeriod = publishedPeriod;

        private void SetPriceRange(PriceRangeViewModel priceRange)
        {
            _model.PriceRange = new PriceRangeModel
            {
                Min = priceRange?.Min,
                Max = priceRange?.Max
            };
        }

        private void SetName(string name) => _model.GameName = name;
    }
}