using Microsoft.AspNetCore.Razor.TagHelpers;
using OnlineGameStore.MVC.Models;
using System.Collections.Generic;

namespace OnlineGameStore.MVC.TagHelpers
{
    public class CommentsTagHelper : TagHelper
    {
        public IEnumerable<CommentViewModel> Elements { get; set; }

        public string ReplyClickMethod { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";

            output.TagMode = TagMode.StartTagAndEndTag;

            output.Attributes.SetAttribute("class", "container scroll-bar");

            output.Attributes.SetAttribute("onclick", ReplyClickMethod);

            var listContent = BuildListContent(Elements);

            static string BuildListContent(IEnumerable<CommentViewModel> elements)
            {
                var listContent = string.Empty;

                foreach (var comment in elements)
                {
                    listContent += "<ul class=\"list-group list-group-flush border-0\">";

                    listContent += "<li class=\"list-group-item\">";

                    listContent += "<div class=\"card\">";

                    listContent += $"<div class=\"card-body\" id=\"comment{comment.Id}\">";

                    listContent += $"<h5 class=\"card-title\">{comment.Name}</h5>";

                    listContent += "<p class=\"card-text\">";

                    if (comment.ReplyToId != null)
                    {
                        listContent += $"<a class=\"text-decoration-none\" href=\"#comment{comment.ReplyToId}\">";
                        listContent += $"{comment.ReplyToAuthor}, </a>";
                    }

                    listContent += $"{comment.Body}</p>";

                    listContent += "<button class=\"btn btn-primary btn-reply\">Reply</button>";

                    listContent += "</div>";

                    if (comment.Replies != null)
                    {
                        listContent += BuildListContent(comment.Replies);
                    }

                    listContent += "</div></li></ul>";
                }

                return listContent;
            }

            output.Content.SetHtmlContent(listContent);
        }
    }
}
