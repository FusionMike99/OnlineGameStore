using OnlineGameStore.DomainModels.Enums;

namespace OnlineGameStore.DomainModels.Models
{
    public class PageModel
    {
        private int _currentPageNumber;

        public int CurrentPageNumber
        {
            get => _currentPageNumber;
            set => _currentPageNumber = value < 1 ? 1 : value;
        }
        
        public PageSize PageSize { get; set; }

        public PageModel(int currentPageNumber, PageSize pageSize)
        {
            CurrentPageNumber = currentPageNumber;
            PageSize = pageSize;
        }
    }
}