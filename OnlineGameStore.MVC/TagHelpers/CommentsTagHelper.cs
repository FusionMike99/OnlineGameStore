using System.Collections.Generic;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using OnlineGameStore.MVC.Models;

namespace OnlineGameStore.MVC.TagHelpers
{
    public class CommentsTagHelper : TagHelper
    {
        private readonly IUrlHelperFactory _urlHelperFactory;
        
        public IEnumerable<CommentViewModel> Elements { get; set; }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        
        [HtmlAttributeName(DictionaryAttributePrefix = "view-action-")]
        public IDictionary<string, object> ActionValues { get; }
        
        [HtmlAttributeName(DictionaryAttributePrefix = "view-url-")]
        public IDictionary<string, object> UrlValues { get; }
        
        public CommentsTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            _urlHelperFactory = urlHelperFactory;

            ActionValues = new Dictionary<string, object>();
            UrlValues = new Dictionary<string, object>();
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);
            
            output.TagName = "div";

            output.TagMode = TagMode.StartTagAndEndTag;

            output.Attributes.SetAttribute("class", "container scroll-bar");

            var htmlContentBuilder = BuildListContent(Elements);

            IHtmlContentBuilder BuildListContent(IEnumerable<CommentViewModel> elements)
            {
                var commentTagBuilder = new TagBuilder("div");

                foreach (var comment in elements)
                {
                    var ulTagBuilder = new TagBuilder("ul");
                    ulTagBuilder.AddCssClass("list-group list-group-flush border-0");

                    var liTagBuilder = new TagBuilder("li");
                    liTagBuilder.AddCssClass("list-group-item");

                    var cardTagBuilder = new TagBuilder("div");
                    cardTagBuilder.AddCssClass("card");
                    
                    var cardBodyTagBuilder = new TagBuilder("div");
                    cardBodyTagBuilder.AddCssClass("card-body");
                    cardBodyTagBuilder.Attributes.Add("id", $"comment{comment.Id}");
                    
                    var cardTitleTagBuilder = new TagBuilder("h5");
                    cardTitleTagBuilder.AddCssClass("card-title");
                    cardTitleTagBuilder.InnerHtml.Append(comment.Name);

                    cardBodyTagBuilder.InnerHtml.AppendHtml(cardTitleTagBuilder);
                    
                    var cardTextTagBuilder = new TagBuilder("p");
                    cardTextTagBuilder.AddCssClass("card-text");

                    if (comment.IsDeleted)
                    {
                        cardTextTagBuilder.AddCssClass("fst-italic");
                    }

                    if (comment.ReplyToId != null)
                    {
                        if (comment.IsQuoted)
                        {
                            var replyLinkTagBuilder = new TagBuilder("custom-quote");
                            replyLinkTagBuilder.Attributes.Add("data-comment-message", comment.ReplyTo.Body);
                            replyLinkTagBuilder.Attributes.Add("data-comment-author", comment.ReplyTo.Name);

                            cardTextTagBuilder.InnerHtml.AppendHtml(replyLinkTagBuilder);
                        }
                        else
                        {
                            var replyLinkTagBuilder = new TagBuilder("a");
                            replyLinkTagBuilder.AddCssClass("text-decoration-none");
                            replyLinkTagBuilder.Attributes.Add("href", $"#comment{comment.ReplyToId}");
                            replyLinkTagBuilder.InnerHtml.Append($"{comment.ReplyTo.Name}, ");

                            cardTextTagBuilder.InnerHtml.AppendHtml(replyLinkTagBuilder);
                        }
                    }

                    cardTextTagBuilder.InnerHtml.Append(comment.Body);
                    
                    cardBodyTagBuilder.InnerHtml.AppendHtml(cardTextTagBuilder);

                    if (!comment.IsDeleted)
                    {
                        var answerButtonTagBuilder = new TagBuilder("button");
                        answerButtonTagBuilder.AddCssClass("btn btn-primary me-2 btn-reply");
                        answerButtonTagBuilder.Attributes.Add("data-comment-kind", "Answer");
                        answerButtonTagBuilder.InnerHtml.Append("Reply");

                        cardBodyTagBuilder.InnerHtml.AppendHtml(answerButtonTagBuilder);
                    
                        var quoteButtonTagBuilder = new TagBuilder("button");
                        quoteButtonTagBuilder.AddCssClass("btn btn-success me-2 btn-reply");
                        quoteButtonTagBuilder.Attributes.Add("data-comment-kind", "Quote");
                        quoteButtonTagBuilder.InnerHtml.Append("Quote");

                        cardBodyTagBuilder.InnerHtml.AppendHtml(quoteButtonTagBuilder);
                    
                        var updateLinkHref = urlHelper.Action(ActionValues["update"].ToString(), "Comment",
                            new { gameKey = UrlValues["gameKey"], commentId = comment.Id });
                    
                        var updateLinkTagBuilder = new TagBuilder("a");
                        updateLinkTagBuilder.AddCssClass("btn btn-info me-2 modal-link");
                        updateLinkTagBuilder.Attributes.Add("href", updateLinkHref);
                        updateLinkTagBuilder.InnerHtml.Append("Update");

                        cardBodyTagBuilder.InnerHtml.AppendHtml(updateLinkTagBuilder);
                    
                        var removeLinkHref = urlHelper.Action(ActionValues["remove"].ToString(), "Comment",
                            new { gameKey = UrlValues["gameKey"], id = comment.Id });
                    
                        var removeLinkTagBuilder = new TagBuilder("a");
                        removeLinkTagBuilder.AddCssClass("btn btn-danger me-2 modal-link");
                        removeLinkTagBuilder.Attributes.Add("href", removeLinkHref);
                        removeLinkTagBuilder.InnerHtml.Append("Delete");

                        cardBodyTagBuilder.InnerHtml.AppendHtml(removeLinkTagBuilder);
                    }
                    
                    var banLinkHref = urlHelper.Action("Ban", "User",
                        new { userName = comment.Name, returnUrl = UrlValues["return"] });
                    
                    var banLinkTagBuilder = new TagBuilder("a");
                    banLinkTagBuilder.AddCssClass("btn btn-outline-danger");
                    banLinkTagBuilder.Attributes.Add("href", banLinkHref);
                    banLinkTagBuilder.InnerHtml.Append("Ban");

                    cardBodyTagBuilder.InnerHtml.AppendHtml(banLinkTagBuilder);

                    cardTagBuilder.InnerHtml.AppendHtml(cardBodyTagBuilder);

                    if (comment.Replies != null)
                    {
                        cardTagBuilder.InnerHtml.AppendHtml(BuildListContent(comment.Replies));
                    }

                    liTagBuilder.InnerHtml.AppendHtml(cardTagBuilder);
                    ulTagBuilder.InnerHtml.AppendHtml(liTagBuilder);

                    commentTagBuilder.InnerHtml.AppendHtml(ulTagBuilder);
                }

                return commentTagBuilder.InnerHtml;
            }

            output.Content.SetHtmlContent(htmlContentBuilder);
        }
    }
}