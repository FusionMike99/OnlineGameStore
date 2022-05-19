using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
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
        public void Update_Get_ReturnsViewResult(
            Comment comment,
            string gameKey,
            [Frozen] Mock<ICommentService> mockCommentService,
            CommentController sut)
        {
            // Arrange
            mockCommentService.Setup(x => x.GetCommentById(It.IsAny<int>()))
                .Returns(comment);

            // Act
            var result = sut.UpdateComment(comment.Id, gameKey);

            // Assert
            result.Should().BeOfType<PartialViewResult>()
                .Which.Model.Should().BeAssignableTo<EditCommentViewModel>()
                .Which.Id.Should().Be(comment.Id);
            
            mockCommentService.Verify(x => x.GetCommentById(It.IsAny<int>()), Times.Once);
        }

        [Theory]
        [InlineAutoMoqData(null)]
        public void Update_Get_ReturnsBadRequestObjectResult_WhenCommentIdHasNotValue(
            int? commentId,
            string gameKey,
            CommentController sut)
        {
            // Act
            var result = sut.UpdateComment(commentId, gameKey);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>()
                .Which.Value.Should().BeOfType<string>();
        }

        [Theory]
        [InlineAutoMoqData(null)]
        public void Update_Get_ReturnsNotFoundObjectResult_WhenCommentIsNotFound(
            Comment comment,
            int? commentId,
            string gameKey,
            [Frozen] Mock<ICommentService> mockCommentService,
            CommentController sut)
        {
            // Arrange
            mockCommentService.Setup(x => x.GetCommentById(It.IsAny<int>()))
                .Returns(comment);

            // Act
            var result = sut.UpdateComment(commentId, gameKey);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>()
                .Which.Value.Should().BeOfType<string>();

            mockCommentService.Verify(x => x.GetCommentById(It.IsAny<int>()), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public void Update_Post_ReturnsRedirectToActionResult_WhenCommentIsValid(
            Comment comment,
            string gameKey,
            [Frozen] Mock<ICommentService> mockCommentService,
            [Frozen] Mock<IUrlHelper> mockUrlHelper,
            CommentController sut)
        {
            // Arrange
            mockCommentService.Setup(x => x.EditComment(It.IsAny<Comment>()))
                .Returns(comment);

            mockUrlHelper.Setup(x => x.Action(It.IsAny<UrlActionContext>()))
                .Returns("TestUrl");

            var editCommentViewModel = new EditCommentViewModel
            {
                Id = comment.Id,
                Name = comment.Name,
                Body = comment.Body,
                IsQuoted = comment.IsQuoted,
                GameId = comment.GameId,
                ReplyToId = comment.ReplyToId
            };

            sut.Url = mockUrlHelper.Object;

            // Act
            var result = sut.UpdateComment(editCommentViewModel.Id, editCommentViewModel, gameKey);

            // Assert
            result.Should().BeOfType<JsonResult>();

            mockCommentService.Verify(x => x.EditComment(It.IsAny<Comment>()), Times.Once);
        }
        
        [Theory]
        [AutoMoqData]
        public void Update_Post_ReturnsNotFoundObjectResult_WhenCommentIsNotFound(
            EditCommentViewModel editCommentViewModel,
            string gameKey,
            CommentController sut)
        {
            // Arrange
            var id = editCommentViewModel.Id - 1;

            // Act
            var result = sut.UpdateComment(id, editCommentViewModel, gameKey);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>()
                .Which.Value.Should().BeOfType<string>();
        }

        [Theory]
        [AutoMoqData]
        public void Update_Post_ReturnsViewResult_WhenCommentIsInvalid(
            EditCommentViewModel editCommentViewModel,
            string gameKey,
            CommentController sut)
        {
            // Arrange
            sut.ModelState.AddModelError("Name", "Required");

            // Act
            var result = sut.UpdateComment(editCommentViewModel.Id, editCommentViewModel, gameKey);

            // Assert
            result.Should().BeOfType<PartialViewResult>()
                .Which.Model.Should().BeAssignableTo<EditCommentViewModel>()
                .Which.Id.Should().Be(editCommentViewModel.Id);
        }
    }
}