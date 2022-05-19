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
        public void Remove_Get_ReturnsViewResult(
            Comment comment,
            string gameKey,
            [Frozen] Mock<ICommentService> mockCommentService,
            CommentController sut)
        {
            // Arrange
            mockCommentService.Setup(x => x.GetCommentById(It.IsAny<int>()))
                .Returns(comment);

            // Act
            var result = sut.RemoveComment(comment.Id, gameKey);

            // Assert
            result.Should().BeOfType<PartialViewResult>()
                .Which.Model.Should().BeAssignableTo<CommentViewModel>()
                .Which.Id.Should().Be(comment.Id);
            
            mockCommentService.Verify(x => x.GetCommentById(It.IsAny<int>()), Times.Once);
        }

        [Theory]
        [InlineAutoMoqData(null)]
        public void Remove_Get_ReturnsBadRequestObjectResult_WhenCommentIdHasNotValue(
            int? commentId,
            string gameKey,
            CommentController sut)
        {
            // Act
            var result = sut.RemoveComment(commentId, gameKey);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>()
                .Which.Value.Should().BeOfType<string>();
        }

        [Theory]
        [InlineAutoMoqData(null)]
        public void Remove_Get_ReturnsNotFoundObjectResult_WhenCommentIsNotFound(
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
            var result = sut.RemoveComment(commentId, gameKey);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>()
                .Which.Value.Should().BeOfType<string>();

            mockCommentService.Verify(x => x.GetCommentById(It.IsAny<int>()), Times.Once);
        }
        
        [Theory]
        [AutoMoqData]
        public void Remove_ReturnsRedirectToActionResult_WhenIdHasValue(
            int id,
            string gameKey,
            [Frozen] Mock<ICommentService> mockCommentService,
            CommentController sut)
        {
            // Arrange
            mockCommentService.Setup(x => x.DeleteComment(It.IsAny<int>()));

            // Act
            var result = sut.RemoveCommentConfirmed(id, gameKey);

            // Assert
            result.Should().BeOfType<RedirectToActionResult>()
                .Subject.ActionName.Should().BeEquivalentTo(nameof(sut.GetCommentsByGameKey));

            mockCommentService.Verify(x => x.DeleteComment(It.IsAny<int>()), Times.Once);
        }

        [Theory]
        [InlineAutoMoqData(null)]
        public void Remove_ReturnsBadRequestObjectResult_WhenIdHasNotValue(
            int? id,
            string gameKey,
            CommentController sut)
        {
            // Act
            var result = sut.RemoveCommentConfirmed(id, gameKey);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>()
                .Which.Value.Should().BeOfType<string>();
        }
    }
}