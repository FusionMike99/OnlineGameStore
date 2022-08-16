using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineGameStore.BLL.Services.Interfaces;
using OnlineGameStore.DomainModels.Enums;
using OnlineGameStore.DomainModels.Models;
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

            await modelBuilder.SetGenres(genreService, sortFilterGameViewModel.SelectedGenres);
            await modelBuilder.SetPlatformTypes(platformTypeService, sortFilterGameViewModel.SelectedPlatformTypes);
            await modelBuilder.SetPublishers(publisherService, sortFilterGameViewModel.SelectedPublishers);
            modelBuilder.SetSorting(sortFilterGameViewModel.GameSortState);
            modelBuilder.SetDatePublished(sortFilterGameViewModel.DatePublishedPeriod);
            modelBuilder.SetPriceRange(sortFilterGameViewModel.PriceRange);
            modelBuilder.SetName(sortFilterGameViewModel.GameName);

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
                var genresTask = genreService.GetGenresIdsByNamesAsync(selectedGenres.ToArray());
                var categoriesTask = genreService.GetCategoriesIdsByNamesAsync(selectedGenres);

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
                    .GetPlatformTypesIdsByNamesAsync(selectedPlatformTypes)).ToList();
            }

            _model.SelectedPlatformTypes = selectedPlatformTypesIds;
        }
        
        private async Task SetPublishers(IPublisherService publisherService, List<string> selectedPublishers)
        {
            List<string> selectedSuppliersIds = null;
            
            if (selectedPublishers?.Any() == true)
            {
                selectedSuppliersIds = (await publisherService.GetSuppliersIdsByNamesAsync(selectedPublishers)).ToList();
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