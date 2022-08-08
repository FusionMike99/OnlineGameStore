using System;
using System.Linq.Expressions;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Repositories.GameStore;
using OnlineGameStore.BLL.Services;
using OnlineGameStore.Tests.Helpers;
using Xunit;

namespace OnlineGameStore.Tests.Services
{
    public partial class CommentServiceTests
    {
        [Theory]
        [AutoMoqData]
        public void CommentService_GetCommentById_ReturnsComment(
            Comment comment,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            CommentService sut)
        {
            // Arrange
            mockUnitOfWork
                .Setup(m => m.Comments.GetSingle(
                    It.IsAny<Expression<Func<Comment, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<string[]>()))
                .Returns(comment);

            // Act
            var actualComment = sut.GetCommentById(comment.Id);

            // Assert
            actualComment.Should().BeEquivalentTo(comment);

            mockUnitOfWork.Verify(x => x.Comments.GetSingle(
                    It.IsAny<Expression<Func<Comment, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<string[]>()),
                Times.Once);
        }
    }
}