using System;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
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
        public async Task Remove_Get_ReturnsViewResult(
            CommentModel comment,
            string gameKey,
            [Frozen] Mock<ICommentService> mockCommentService,
            CommentController sut)
        {
            // Arrange
            mockCommentService.Setup(x => x.GetCommentById(It.IsAny<Guid>()))
                .ReturnsAsync(comment);

            // Act
            var result = await sut.RemoveComment(comment.Id, gameKey);

            // Assert
            result.Should().BeOfType<PartialViewResult>()
                .Which.Model.Should().BeAssignableTo<CommentViewModel>()
                .Which.Id.Should().Be(comment.Id);
            
            mockCommentService.Verify(x => x.GetCommentById(It.IsAny<Guid>()), Times.Once);
        }

        [Theory]
        [InlineAutoMoqData(null)]
        public async Task Remove_Get_ReturnsNotFoundResult_WhenCommentIsNotFound(
            CommentModel comment,
            Guid commentId,
            string gameKey,
            [Frozen] Mock<ICommentService> mockCommentService,
            CommentController sut)
        {
            // Arrange
            mockCommentService.Setup(x => x.GetCommentById(It.IsAny<Guid>()))
                .ReturnsAsync(comment);

            // Act
            var result = await sut.RemoveComment(commentId, gameKey);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            mockCommentService.Verify(x => x.GetCommentById(It.IsAny<Guid>()), Times.Once);
        }
        
        [Theory]
        [AutoMoqData]
        public async Task Remove_ReturnsRedirectToActionResult(
            Guid id,
            string gameKey,
            [Frozen] Mock<ICommentService> mockCommentService,
            CommentController sut)
        {
            // Arrange
            mockCommentService.Setup(x => x.DeleteComment(It.IsAny<Guid>()));

            // Act
            var result = await sut.RemoveCommentConfirmed(id, gameKey);

            // Assert
            result.Should().BeOfType<RedirectToActionResult>()
                .Subject.ActionName.Should().BeEquivalentTo(nameof(sut.GetCommentsByGameKey));

            mockCommentService.Verify(x => x.DeleteComment(It.IsAny<Guid>()), Times.Once);
        }
    }
}