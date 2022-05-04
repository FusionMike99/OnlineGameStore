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
            result.Should().BeOfType<RedirectToActionResult>()
                .Subject.ActionName.Should().BeEquivalentTo(nameof(sut.GetCommentsByGameKey));

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
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<AggregateCommentViewModel>()
                    .Which.EditComment.Name.Should().BeEquivalentTo(editCommentViewModel.Name);
        }

        [Theory]
        [AutoMoqData]
        public void NewComment_ReturnsViewResult_WhenCommentIsInvalid(
            string gameKey,
            EditCommentViewModel editCommentViewModel,
            CommentController sut)
        {
            // Arrange
            sut.ModelState.AddModelError("Name", "Required");

            // Act
            var result = sut.NewComment(gameKey, editCommentViewModel);

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<AggregateCommentViewModel>()
                    .Which.EditComment.Name.Should().BeEquivalentTo(editCommentViewModel.Name);
        }
    }
}
