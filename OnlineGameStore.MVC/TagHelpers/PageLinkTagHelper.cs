using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using OnlineGameStore.MVC.Models;

namespace OnlineGameStore.MVC.TagHelpers
{
    public class PageLinkTagHelper : TagHelper
    {
        private readonly IUrlHelperFactory _urlHelperFactory;

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public PageViewModel PageViewModel { get; set; }

        public string PageAction { get; set; }
        
        public int Capacity { get; set; }

        public PageLinkTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            _urlHelperFactory = urlHelperFactory;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (ViewContext == null || PageViewModel == null ||
                PageViewModel.TotalPages < 2 || PageViewModel.CurrentPageNumber > PageViewModel.TotalPages)
            {
                return;
            }
            
            var urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);
            output.TagName = "ul";
            
            output.Attributes.SetAttribute("class", "pagination justify-content-center");

            var initialPageNumber = 1;

            var totalPages = PageViewModel.TotalPages;

            if (totalPages > Capacity)
            {
                var capacityHalf = Capacity / 2 - 1;
                
                if (PageViewModel.CurrentPageNumber < Capacity)
                {
                    totalPages = Capacity;
                    
                    GeneratePageLinkList();
                    
                    GenerateLastPageLinkWithDots();
                }
                else if(PageViewModel.CurrentPageNumber <= PageViewModel.TotalPages - capacityHalf)
                {
                    initialPageNumber = PageViewModel.CurrentPageNumber - capacityHalf;

                    totalPages = PageViewModel.CurrentPageNumber + capacityHalf;

                    GenerateFirstPageLinkWithDots();

                    GeneratePageLinkList();
                    
                    GenerateLastPageLinkWithDots();
                }
                else
                {
                    initialPageNumber = PageViewModel.TotalPages - Capacity + 1;
                    
                    GenerateFirstPageLinkWithDots();
                    
                    GeneratePageLinkList();
                }
            }
            else
            {
                GeneratePageLinkList();
            }

            if (totalPages > 1)
            {
                output.Content.AppendHtml(CreateInputExactPageNumber());
            }

            void GenerateFirstPageLinkWithDots()
            {
                var firstPage = CreatePageLink(1, urlHelper);
                    
                var tripleDot = CreateTripleDot();

                output.Content.AppendHtml(firstPage);
                    
                output.Content.AppendHtml(tripleDot);
            }

            void GeneratePageLinkList()
            {
                for (; initialPageNumber <= totalPages; initialPageNumber++)
                {
                    var currentItem = CreatePageLink(initialPageNumber, urlHelper);
                
                    output.Content.AppendHtml(currentItem);
                }
            }

            void GenerateLastPageLinkWithDots()
            {
                var tripleDot = CreateTripleDot();
                
                var lastPage = CreatePageLink(PageViewModel.TotalPages, urlHelper);
                    
                output.Content.AppendHtml(tripleDot);

                output.Content.AppendHtml(lastPage);
            }
        }

        private TagBuilder CreatePageLink(int pageNumber, IUrlHelper urlHelper)
        {
            var item = new TagBuilder("li");
            TagBuilder link;
            
            if (pageNumber == PageViewModel.CurrentPageNumber)
            {
                item.AddCssClass("active");

                link = new TagBuilder("span");
            }
            else
            {
                link = new TagBuilder("a")
                {
                    Attributes =
                    {
                        ["href"] = urlHelper.Action(PageAction, new { PageNumber = pageNumber })
                    }
                };
            }
            
            item.AddCssClass("page-item");
            link.AddCssClass("page-link");
            
            link.InnerHtml.Append(pageNumber.ToString());
            item.InnerHtml.AppendHtml(link);
            
            return item;
        }
        
        private TagBuilder CreateTripleDot()
        {
            var item = new TagBuilder("li");
            
            var link = new TagBuilder("a")
            {
                Attributes =
                {
                    ["href"] = "#"
                }
            };

            item.AddCssClass("page-item disabled");
            link.AddCssClass("page-link");
            
            link.InnerHtml.Append("...");
            item.InnerHtml.AppendHtml(link);
            
            return item;
        }

        private TagBuilder CreateInputExactPageNumber()
        {
            var item = new TagBuilder("li");
            item.AddCssClass("page-item ms-2");
            
            var div = new TagBuilder("div")
            {
                Attributes =
                {
                    ["class"] = "input-group",
                }
            };
            
            var input = new TagBuilder("input")
            {
                Attributes =
                {
                    ["id"] = "PageNumber",
                    ["name"] = "PageNumber",
                    ["type"] = "number",
                    ["min"] = "1",
                    ["max"] = PageViewModel.TotalPages.ToString(),
                    ["form"] = "sort-filter-form",
                    ["data-bs-toggle"] = "tooltip",
                    ["title"] = "Please press Enter!",
                    ["placeholder"] = "Pg.",
                    ["class"] = "form-control"
                }
            };
            
            div.InnerHtml.AppendHtml(input);

            item.InnerHtml.AppendHtml(div);
            
            return item;
        }
    }
}