using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineGameStore.BLL.Services.Interfaces;
using OnlineGameStore.DomainModels.Models.General;
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
        public async Task NewComment_ReturnsJsonResult_WhenGameKeyHasValueAndCommentIsValid(
            CommentModel comment,
            string gameKey,
            [Frozen] Mock<ICommentService> mockCommentService,
            CommentController sut)
        {
            // Arrange
            mockCommentService.Setup(x => x.LeaveCommentToGameAsync(It.IsAny<string>(),
                    It.IsAny<CommentModel>()))
                .ReturnsAsync(comment);

            var editCommentViewModel = new EditCommentViewModel
            {
                Name = comment.Name,
                Body = comment.Body,
                ReplyToId = comment.ReplyToId
            };

            // Act
            var result = await sut.NewComment(gameKey, editCommentViewModel);

            // Assert
            result.Should().BeOfType<RedirectToActionResult>()
                .Subject.ActionName.Should().BeEquivalentTo(nameof(sut.GetCommentsByGameKey));

            mockCommentService.Verify(x => x.LeaveCommentToGameAsync(It.IsAny<string>(),
                    It.IsAny<CommentModel>()),
                Times.Once);
        }

        [Theory]
        [InlineAutoMoqData("")]
        [InlineAutoMoqData(" ")]
        [InlineAutoMoqData(null)]
        public async Task NewComment_ReturnsBadRequestObjectResult_WhenGameKeyHasNotValue(
            string gameKey,
            EditCommentViewModel editCommentViewModel,
            CommentController sut)
        {
            // Act
            var result = await sut.NewComment(gameKey, editCommentViewModel);

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<AggregateCommentViewModel>()
                .Which.EditComment.Name.Should().BeEquivalentTo(editCommentViewModel.Name);
        }

        [Theory]
        [AutoMoqData]
        public async Task NewComment_ReturnsViewResult_WhenCommentIsInvalid(
            string gameKey,
            EditCommentViewModel editCommentViewModel,
            CommentController sut)
        {
            // Arrange
            sut.ModelState.AddModelError("Name", "Required");

            // Act
            var result = await sut.NewComment(gameKey, editCommentViewModel);

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<AggregateCommentViewModel>()
                .Which.EditComment.Name.Should().BeEquivalentTo(editCommentViewModel.Name);
        }
    }
}