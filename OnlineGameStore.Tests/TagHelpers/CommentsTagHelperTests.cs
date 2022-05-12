using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
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
            var commentsTagHelper = new CommentsTagHelper
            {
                Elements = GetTestComments(),
                ReplyClickMethod = string.Empty
            };

            const string expected = "<ul class=\"list-group list-group-flush border-0\">" +
                                    "<li class=\"list-group-item\">" +
                                    "<div class=\"card\">" +
                                    "<div class=\"card-body\" id=\"comment1\">" +
                                    "<h5 class=\"card-title\">Name1</h5>" +
                                    "<p class=\"card-text\">Body1</p>" +
                                    "<button class=\"btn btn-primary btn-reply\">Reply</button></div>" +
                                    "<ul class=\"list-group list-group-flush border-0\">" +
                                    "<li class=\"list-group-item\">" +
                                    "<div class=\"card\">" +
                                    "<div class=\"card-body\" id=\"comment2\">" +
                                    "<h5 class=\"card-title\">Name2</h5>" +
                                    "<p class=\"card-text\">" +
                                    "<a class=\"text-decoration-none\" href=\"#comment1\">Name1, </a>Body2</p>" +
                                    "<button class=\"btn btn-primary btn-reply\">Reply</button>" +
                                    "</div></div></li></ul></div></li></ul>";

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
            var comments = new List<CommentViewModel>
            {
                new CommentViewModel
                {
                    Id = 1,
                    Name = "Name1",
                    Body = "Body1",
                    ReplyToId = null,
                    ReplyToAuthor = string.Empty,
                    Replies = new List<CommentViewModel>
                    {
                        new CommentViewModel
                        {
                            Id = 2,
                            Name = "Name2",
                            Body = "Body2",
                            ReplyToId = 1,
                            ReplyToAuthor = "Name1",
                            Replies = null
                        }
                    }
                }
            };

            return comments;
        }
    }
}