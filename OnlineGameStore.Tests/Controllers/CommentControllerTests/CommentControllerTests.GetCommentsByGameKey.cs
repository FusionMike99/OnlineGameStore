using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Services.Contracts;
using OnlineGameStore.MVC.Controllers;
using OnlineGameStore.MVC.Models;
using OnlineGameStore.Tests.Helpers;
using System.Collections.Generic;
using Xunit;

namespace OnlineGameStore.Tests.Controllers
{
    public partial class CommentControllerTests
    {
        [Theory]
        [AutoMoqData]
        public void GetCommentsByGameKey_ReturnsViewResult_WhenGameKeyHasValue(
            IEnumerable<Comment> comments,
            string gameKey,
            [Frozen] Mock<ICommentService> mockCommentService,
            CommentController sut)
        {
            // Arrange
            mockCommentService.Setup(x => x.GetAllCommentsByGameKey(It.IsAny<string>()))
                .Returns(comments);

            // Act
            var result = sut.GetCommentsByGameKey(gameKey);

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<AggregateCommentViewModel>()
                .Which.Comments.Should().HaveSameCount(comments);

            mockCommentService.Verify(x => x.GetAllCommentsByGameKey(It.IsAny<string>()), Times.Once);
        }

        [Theory]
        [InlineAutoMoqData("")]
        [InlineAutoMoqData(" ")]
        [InlineAutoMoqData(null)]
        public void GetCommentsByGameKey_ReturnsBadRequestObjectResult_WhenGameKeyHasNotValue(
            string gameKey,
            CommentController sut)
        {
            // Act
            var result = sut.GetCommentsByGameKey(gameKey);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>()
                .Which.Value.Should().BeOfType<string>();
        }
    }
}
