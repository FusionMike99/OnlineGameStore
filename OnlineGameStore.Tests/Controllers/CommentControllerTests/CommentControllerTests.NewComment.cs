using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Services.Contracts;
using OnlineGameStore.MVC.Controllers;
using OnlineGameStore.MVC.Models;
using OnlineGameStore.Tests.Helpers;
using Xunit;

namespace OnlineGameStore.Tests.Controllers
{
    public partial class CommentControllerTests
    {
        [Theory]
        [AutoMoqData]
        public void NewComment_ReturnsJsonResult_WhenGameKeyHasValueAndCommentIsValid(
               Comment comment,
               string gameKey,
               [Frozen] Mock<ICommentService> mockCommentService,
               CommentController sut)
        {
            // Arrange
            mockCommentService.Setup(x => x.LeaveCommentToGame(
                    It.IsAny<string>(),
                    It.IsAny<Comment>()))
                .Returns(comment);

            var editCommentViewModel = new EditCommentViewModel
            {
                Name = comment.Name,
                Body = comment.Body,
                ReplyToId = comment.ReplyToId
            };

            // Act
            var result = sut.NewComment(gameKey, editCommentViewModel);

            // Assert
            result.Should().BeOfType<JsonResult>()
                .Which.Value.Should().BeAssignableTo<CommentViewModel>()
                .Which.Name.Should().Be(editCommentViewModel.Name);

            mockCommentService.Verify(x => x.LeaveCommentToGame(
                It.IsAny<string>(),
                It.IsAny<Comment>()),
                Times.Once);
        }

        [Theory]
        [InlineAutoMoqData("")]
        [InlineAutoMoqData(" ")]
        [InlineAutoMoqData(null)]
        public void NewComment_ReturnsBadRequestObjectResult_WhenGameKeyHasNotValue(
            string gameKey,
            EditCommentViewModel editCommentViewModel,
            CommentController sut)
        {
            // Act
            var result = sut.NewComment(gameKey, editCommentViewModel);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>()
                .Which.Value.Should().BeOfType<string>();
        }

        [Theory]
        [InlineAutoMoqData("", null)]
        public void NewComment_ReturnsBadRequestObjectResult_WhenCommentIsInvalid(
            string gameKey,
            EditCommentViewModel editCommentViewModel,
            CommentController sut)
        {
            // Arrange
            sut.ModelState.AddModelError("Name", "Required");

            // Act
            var result = sut.NewComment(gameKey, editCommentViewModel);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>()
                .Which.Value.Should().BeOfType<string>();
        }
    }
}
