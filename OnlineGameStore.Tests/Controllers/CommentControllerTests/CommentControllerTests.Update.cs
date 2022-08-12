using System;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Moq;
using OnlineGameStore.BLL.Models.General;
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
        public async Task Update_Get_ReturnsViewResult(
            CommentModel comment,
            string gameKey,
            [Frozen] Mock<ICommentService> mockCommentService,
            CommentController sut)
        {
            // Arrange
            mockCommentService.Setup(x => x.GetCommentByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(comment);

            // Act
            var result = await sut.UpdateComment(comment.Id, gameKey);

            // Assert
            result.Should().BeOfType<PartialViewResult>()
                .Which.Model.Should().BeAssignableTo<EditCommentViewModel>()
                .Which.Id.Should().Be(comment.Id);
            
            mockCommentService.Verify(x => x.GetCommentByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Theory]
        [InlineAutoMoqData(null)]
        public async Task Update_Get_ReturnsNotFoundResult_WhenCommentIsNotFound(
            CommentModel comment,
            Guid commentId,
            string gameKey,
            [Frozen] Mock<ICommentService> mockCommentService,
            CommentController sut)
        {
            // Arrange
            mockCommentService.Setup(x => x.GetCommentByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(comment);

            // Act
            var result = await sut.UpdateComment(commentId, gameKey);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            mockCommentService.Verify(x => x.GetCommentByIdAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public async Task Update_Post_ReturnsRedirectToActionResult_WhenCommentIsValid(
            CommentModel comment,
            string gameKey,
            [Frozen] Mock<ICommentService> mockCommentService,
            [Frozen] Mock<IUrlHelper> mockUrlHelper,
            CommentController sut)
        {
            // Arrange
            mockCommentService.Setup(x => x.EditCommentAsync(It.IsAny<CommentModel>()))
                .ReturnsAsync(comment);

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
            var result = await sut.UpdateComment(editCommentViewModel.Id, editCommentViewModel, gameKey);

            // Assert
            result.Should().BeOfType<JsonResult>();

            mockCommentService.Verify(x => x.EditCommentAsync(It.IsAny<CommentModel>()), Times.Once);
        }
        
        [Theory]
        [AutoMoqData]
        public async Task Update_Post_ReturnsNotFoundResult_WhenCommentIsNotFound(
            Guid id,
            EditCommentViewModel editCommentViewModel,
            string gameKey,
            CommentController sut)
        {
            // Act
            var result = await sut.UpdateComment(id, editCommentViewModel, gameKey);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Theory]
        [AutoMoqData]
        public async Task Update_Post_ReturnsViewResult_WhenCommentIsInvalid(
            EditCommentViewModel editCommentViewModel,
            string gameKey,
            CommentController sut)
        {
            // Arrange
            sut.ModelState.AddModelError("Name", "Required");

            // Act
            var result = await sut.UpdateComment(editCommentViewModel.Id, editCommentViewModel, gameKey);

            // Assert
            result.Should().BeOfType<PartialViewResult>()
                .Which.Model.Should().BeAssignableTo<EditCommentViewModel>()
                .Which.Id.Should().Be(editCommentViewModel.Id);
        }
    }
}