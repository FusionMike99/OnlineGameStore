using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineGameStore.DomainModels.Enums;

namespace OnlineGameStore.MVC.Models
{
    public class PageViewModel
    {
        public SelectList PageSizeParameters { get; set; }
        
        public PageSize PageSize { get; set; }
        
        public int CurrentPageNumber { get; set; }

        public int TotalPages { get; set; }
    }
}