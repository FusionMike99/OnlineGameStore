using System;
using System.Linq.Expressions;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Services;
using OnlineGameStore.Tests.Helpers;
using Xunit;

namespace OnlineGameStore.Tests.Services
{
    public partial class CommentServiceTests
    {
        [Theory]
        [AutoMoqData]
        public void CommentService_DeleteComment_DeletesComment(
            Comment comment,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            CommentService sut)
        {
            // Arrange
            mockUnitOfWork
                .Setup(m => m.Comments.GetSingle(
                    It.IsAny<Expression<Func<Comment, bool>>>(),
                    It.IsAny<bool>()))
                .Returns(comment);

            mockUnitOfWork.Setup(x => x.Comments.Delete(It.IsAny<Comment>()));

            // Act
            sut.DeleteComment(comment.Id);

            // Assert
            mockUnitOfWork.Verify(x => x.Comments.GetSingle(It.IsAny<Expression<Func<Comment, bool>>>(),
                    It.IsAny<bool>()),
                Times.Once);
            mockUnitOfWork.Verify(x => x.Comments.Delete(It.Is<Comment>(c => c.Id == comment.Id)),
                Times.Once);
            mockUnitOfWork.Verify(x => x.Commit(), Times.Once);
        }

        [Theory]
        [InlineAutoMoqData(null)]
        public void CommentService_DeleteComment_ThrowsInvalidOperationExceptionWithNullEntity(
            Comment comment,
            int commentId,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            CommentService sut)
        {
            // Arrange
            mockUnitOfWork
                .Setup(m => m.Comments.GetSingle(
                    It.IsAny<Expression<Func<Comment, bool>>>(),
                    It.IsAny<bool>()))
                .Returns(comment);

            // Act
            Action actual = () => sut.DeleteComment(commentId);

            // Assert
            actual.Should().Throw<InvalidOperationException>();

            mockUnitOfWork.Verify(x => x.Comments.GetSingle(
                    It.IsAny<Expression<Func<Comment, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<string[]>()),
                Times.Once);
        }
    }
}