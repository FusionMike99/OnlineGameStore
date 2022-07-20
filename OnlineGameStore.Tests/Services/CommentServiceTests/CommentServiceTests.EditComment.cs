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
        public void CommentService_EditComment_ReturnsComment(
            Comment comment,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            CommentService sut)
        {
            // Arrange
            mockUnitOfWork.Setup(x => x.Comments.Update(It.IsAny<Comment>(),
                    It.IsAny<Expression<Func<Comment,bool>>>()))
                .Returns(comment);

            // Act
            var actualComment = sut.EditComment(comment);

            // Assert
            actualComment.Should().BeEquivalentTo(comment);

            mockUnitOfWork.Verify(x => x.Comments.Update(It.IsAny<Comment>(),
                It.IsAny<Expression<Func<Comment,bool>>>()), Times.Once);
            mockUnitOfWork.Verify(x => x.Commit(), Times.Once);
        }
    }
}