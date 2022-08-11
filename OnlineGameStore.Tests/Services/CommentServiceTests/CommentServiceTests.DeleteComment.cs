using System;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using OnlineGameStore.BLL.Models.General;
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
        public async Task CommentService_DeleteComment_DeletesComment(
            CommentModel comment,
            [Frozen] Mock<ICommentRepository> commentRepositoryMock,
            CommentService sut)
        {
            // Arrange
            commentRepositoryMock
                .Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>(), It.IsAny<string[]>()))
                .ReturnsAsync(comment);

            commentRepositoryMock.Setup(x => x.DeleteAsync(It.IsAny<CommentModel>()));

            // Act
            await sut.DeleteComment(comment.Id);

            // Assert
            commentRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>(),
                It.IsAny<string[]>()), Times.Once);
            commentRepositoryMock.Verify(x => x.DeleteAsync(
                    It.Is<CommentModel>(c => c.Id == comment.Id)), Times.Once);
        }

        [Theory]
        [InlineAutoMoqData(null)]
        public async Task CommentService_DeleteComment_ThrowsInvalidOperationExceptionWithNullEntity(
            CommentModel comment,
            Guid commentId,
            [Frozen] Mock<ICommentRepository> commentRepositoryMock,
            CommentService sut)
        {
            // Arrange
            commentRepositoryMock
                .Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>(), It.IsAny<string[]>()))
                .ReturnsAsync(comment);

            // Act
            Func<Task> actual = async () => await sut.DeleteComment(commentId);

            // Assert
            await actual.Should().ThrowAsync<InvalidOperationException>();

            commentRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>(),
                It.IsAny<string[]>()), Times.Once);
        }
    }
}