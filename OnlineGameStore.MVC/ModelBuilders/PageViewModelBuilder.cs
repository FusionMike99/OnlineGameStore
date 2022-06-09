using System;
using OnlineGameStore.BLL.Enums;
using OnlineGameStore.BLL.Models;
using OnlineGameStore.MVC.Infrastructure;
using OnlineGameStore.MVC.Models;

namespace OnlineGameStore.MVC.ModelBuilders
{
    public class PageViewModelBuilder
    {
        private readonly PageViewModel _model;

        public PageViewModelBuilder(PageModel model, int count)
        {
            _model = new PageViewModel();
            
            SetPageNumber(model.CurrentPageNumber);
            SetPageSize(model.PageSize);
            SetTotalPages(count);
        }
        
        public static implicit operator PageViewModel(PageViewModelBuilder builder) =>
            builder._model;

        private void SetPageNumber(int pageNumber) => _model.CurrentPageNumber = pageNumber;

        private void SetPageSize(PageSize pageSize)
        {
            _model.PageSize = pageSize;

            _model.PageSizeParameters = pageSize.ToSelectList();
        }
        
        private void SetTotalPages(int count)
        {
            _model.TotalPages = _model.PageSize == PageSize.All
                ? 1
                : (int)Math.Ceiling((decimal)count / (int)_model.PageSize);
        }
    }
}