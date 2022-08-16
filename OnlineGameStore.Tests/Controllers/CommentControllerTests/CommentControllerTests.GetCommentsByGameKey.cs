using System.Collections.Generic;
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
        public async Task GetCommentsByGameKey_ReturnsViewResult_WhenGameKeyHasValue(
            List<CommentModel> comments,
            string gameKey,
            [Frozen] Mock<ICommentService> mockCommentService,
            CommentController sut)
        {
            // Arrange
            mockCommentService.Setup(x => x.GetAllCommentsByGameKeyAsync(It.IsAny<string>()))
                .ReturnsAsync(comments);

            // Act
            var result = await sut.GetCommentsByGameKey(gameKey);

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<AggregateCommentViewModel>()
                .Which.Comments.Should().HaveSameCount(comments);

            mockCommentService.Verify(x => x.GetAllCommentsByGameKeyAsync(It.IsAny<string>()), Times.Once);
        }

        [Theory]
        [InlineAutoMoqData("")]
        [InlineAutoMoqData(" ")]
        [InlineAutoMoqData(null)]
        public async Task GetCommentsByGameKey_ReturnsBadRequestResult_WhenGameKeyHasNotValue(
            string gameKey,
            CommentController sut)
        {
            // Act
            var result = await sut.GetCommentsByGameKey(gameKey);

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }
    }
}