using System.Collections.Generic;
using System.Linq;
using OnlineGameStore.BLL.Enums;
using OnlineGameStore.BLL.Models;
using OnlineGameStore.BLL.Services.Contracts;
using OnlineGameStore.MVC.Models;

namespace OnlineGameStore.MVC.ModelBuilders
{
    public class SortFilterGameModelBuilder
    {
        private readonly SortFilterGameModel _model;
        
        public SortFilterGameModelBuilder(SortFilterGameViewModel sortFilterGameViewModel,
            IGenreService genreService,
            IPlatformTypeService platformTypeService,
            IPublisherService publisherService)
        {
            _model = new SortFilterGameModel();

            SetGenres(genreService, sortFilterGameViewModel.SelectedGenres);
            SetPlatformTypes(platformTypeService, sortFilterGameViewModel.SelectedPlatformTypes);
            SetPublishers(publisherService, sortFilterGameViewModel.SelectedPublishers);
            
            SetSorting(sortFilterGameViewModel.GameSortState);
            SetDatePublished(sortFilterGameViewModel.DatePublishedPeriod);
            
            SetPriceRange(sortFilterGameViewModel.PriceRange);
            SetName(sortFilterGameViewModel.GameName);
        }
        
        public static implicit operator SortFilterGameModel(SortFilterGameModelBuilder builder) =>
            builder._model;
        
        private void SetGenres(IGenreService genreService, ICollection<string> selectedGenres)
        {
            List<string> selectedGenresIds = null;
            List<string> selectedCategoriesIds = null;
            
            if (selectedGenres?.Any() == true)
            {
                selectedGenresIds = genreService.GetGenresIdsByNames(selectedGenres.ToArray()).ToList();
                
                selectedCategoriesIds = genreService.GetCategoriesIdsByNames(selectedGenres).ToList();
            }

            _model.SelectedGenres = selectedGenresIds;
            _model.SelectedCategories = selectedCategoriesIds;
        }
        
        private void SetPlatformTypes(IPlatformTypeService platformTypeService, ICollection<string> selectedPlatformTypes)
        {
            List<string> selectedPlatformTypesIds = null;
            
            if (selectedPlatformTypes?.Any() == true)
            {
                selectedPlatformTypesIds = platformTypeService.GetPlatformTypesIdsByNames(selectedPlatformTypes).ToList();
            }

            _model.SelectedPlatformTypes = selectedPlatformTypesIds;
        }
        
        private void SetPublishers(IPublisherService publisherService, List<string> selectedPublishers)
        {
            List<string> selectedSuppliersIds = null;
            
            if (selectedPublishers?.Any() == true)
            {
                selectedSuppliersIds = publisherService.GetSuppliersIdsByNames(selectedPublishers).ToList();
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