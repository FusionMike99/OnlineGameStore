using System;
using System.Collections.Generic;
using System.Linq;
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
        public void CommentService_GetAllCommentsByGameKey_ReturnsComments(
            string gameKey,
            IEnumerable<Comment> comments,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            CommentService sut)
        {
            // Arrange
            mockUnitOfWork
                .Setup(m => m.Comments.GetMany(
                    It.IsAny<Expression<Func<Comment, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<Func<IQueryable<Comment>,IOrderedQueryable<Comment>>>(),
                    It.IsAny<int?>(),
                    It.IsAny<int?>(),
                    It.IsAny<string[]>()))
                .Returns(comments);

            var expectedComments = comments.Where(c => !c.ReplyToId.HasValue).ToList();

            // Act
            var actualComments = sut.GetAllCommentsByGameKey(gameKey);

            // Assert
            actualComments.Should().BeEquivalentTo(expectedComments);

            mockUnitOfWork.Verify(x => x.Comments.GetMany(
                    It.IsAny<Expression<Func<Comment, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<Func<IQueryable<Comment>,IOrderedQueryable<Comment>>>(),
                    It.IsAny<int?>(),
                    It.IsAny<int?>(),
                    It.IsAny<string[]>()),
                Times.Once);
        }
    }
}