using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Moq;
using OnlineGameStore.MVC.Models;
using OnlineGameStore.MVC.TagHelpers;
using Xunit;

namespace OnlineGameStore.Tests.TagHelpers
{
    public class CommentsTagHelperTests
    {
        [Fact]
        public void Can_Generate_Comments_View()
        {
            // Arrange
            var urlHelper = new Mock<IUrlHelper>();
            urlHelper.Setup(x => x.Action(It.IsAny<UrlActionContext>()))
                .Returns("test-link");

            var urlHelperFactory = new Mock<IUrlHelperFactory>();
            urlHelperFactory.Setup(f => f.GetUrlHelper(It.IsAny<ActionContext>()))
                .Returns(urlHelper.Object);
            
            var viewContext = new Mock<ViewContext>();
            
            var commentsTagHelper = new CommentsTagHelper(urlHelperFactory.Object)
            {
                Elements = GetTestComments(),
                ViewContext = viewContext.Object,
                ActionValues =
                {
                    { "update", "Test/PageUpdate" },
                    { "remove", "Test/PageRemove" }
                },
                UrlValues =
                {
                    { "gameKey", "test-game" },
                    { "return", "test-return" }
                }
            };

            const string expected = "<ul class=\"list-group list-group-flush border-0\">" +
                                    "<li class=\"list-group-item\">" +
                                    "<div class=\"card\">" +
                                    "<div class=\"card-body\" id=\"comment68971d03-15b3-4245-a8b9-d8aee9d34277\">" +
                                    "<h5 class=\"card-title\">Name1</h5>" +
                                    "<p class=\"fst-italic card-text\">Body1</p>" +
                                    "<a class=\"btn btn-outline-danger\"" +
                                    " href=\"test-link\">Ban</a></div>" +
                                    "<ul class=\"list-group list-group-flush border-0\">" +
                                    "<li class=\"list-group-item\">" +
                                    "<div class=\"card\">" +
                                    "<div class=\"card-body\" id=\"comment1759d217-a9f2-4ce2-9c61-c27556d03921\">" +
                                    "<h5 class=\"card-title\">Name2</h5>" +
                                    "<p class=\"card-text\">" +
                                    "<a class=\"text-decoration-none\" href=\"#comment68971d03-15b3-4245-a8b9-d8aee9d34277\">" +
                                    "Name1, </a>Body2</p><button class=\"btn btn-primary me-2 btn-reply\"" +
                                    " data-comment-kind=\"Answer\">Reply</button>" +
                                    "<button class=\"btn btn-success me-2 btn-reply\"" +
                                    " data-comment-kind=\"Quote\">Quote</button>" +
                                    "<a class=\"btn btn-info me-2 modal-link\"" +
                                    " href=\"test-link\">Update</a>" +
                                    "<a class=\"btn btn-danger me-2 modal-link\"" +
                                    " href=\"test-link\">Delete</a>" +
                                    "<a class=\"btn btn-outline-danger\"" +
                                    " href=\"test-link\">Ban</a></div>" +
                                    "</div></li></ul>" +
                                    "<ul class=\"list-group list-group-flush border-0\">" +
                                    "<li class=\"list-group-item\">" +
                                    "<div class=\"card\">" +
                                    "<div class=\"card-body\" id=\"comment7abd08d4-7e02-48e5-8a1f-d3eb7e8f226d\">" +
                                    "<h5 class=\"card-title\">Name3</h5>" +
                                    "<p class=\"card-text\">" +
                                    "<custom-quote data-comment-author=\"Name1\"" +
                                    " data-comment-message=\"Body1\"></custom-quote>Body3</p>" +
                                    "<button class=\"btn btn-primary me-2 btn-reply\"" +
                                    " data-comment-kind=\"Answer\">Reply</button>" +
                                    "<button class=\"btn btn-success me-2 btn-reply\"" +
                                    " data-comment-kind=\"Quote\">Quote</button>" +
                                    "<a class=\"btn btn-info me-2 modal-link\"" +
                                    " href=\"test-link\">Update</a>" +
                                    "<a class=\"btn btn-danger me-2 modal-link\"" +
                                    " href=\"test-link\">Delete</a>" +
                                    "<a class=\"btn btn-outline-danger\"" +
                                    " href=\"test-link\">Ban</a></div>" +
                                    "</div></li></ul></div></li></ul>";

            var tagHelperContext = new TagHelperContext(new TagHelperAttributeList(),
                new Dictionary<object, object>(), "");

            var mockTagHelperContent = new Mock<TagHelperContent>();
            var tagHelperOutput = new TagHelperOutput("div", new TagHelperAttributeList(),
                (cache, encoder) => Task.FromResult(mockTagHelperContent.Object));

            // Act
            commentsTagHelper.Process(tagHelperContext, tagHelperOutput);

            // Assert
            var content = tagHelperOutput.Content.GetContent();
            content.Should().Be(expected);
        }

        private static IEnumerable<CommentViewModel> GetTestComments()
        {
            var comment1 = new CommentViewModel
            {
                Id = Guid.Parse("68971D03-15B3-4245-A8B9-D8AEE9D34277"),
                Name = "Name1",
                Body = "Body1",
                ReplyToId = null,
                ReplyTo = null,
                Replies = new List<CommentViewModel>(),
                IsDeleted = true
            };

            var comment2 = new CommentViewModel
            {
                Id = Guid.Parse("1759D217-A9F2-4CE2-9C61-C27556D03921"),
                Name = "Name2",
                Body = "Body2",
                ReplyToId = comment1.Id,
                ReplyTo = comment1,
                Replies = null,
                IsQuoted = false
            };
            
            var comment3 = new CommentViewModel
            {
                Id = Guid.Parse("7ABD08D4-7E02-48E5-8A1F-D3EB7E8F226D"),
                Name = "Name3",
                Body = "Body3",
                ReplyToId = comment1.Id,
                ReplyTo = comment1,
                Replies = null,
                IsQuoted = true
            };

            comment1.Replies = comment1.Replies.Append(comment2);
            comment1.Replies = comment1.Replies.Append(comment3);
            
            var comments = new List<CommentViewModel>
            {
                comment1
            };

            return comments;
        }
    }
}