using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Services;
using OnlineGameStore.BLL.Services.Contracts;
using OnlineGameStore.Tests.Helpers;
using Xunit;

namespace OnlineGameStore.Tests.Services
{
    public partial class CommentServiceTests
    {
        [Theory]
        [AutoMoqData]
        public void CommentService_LeaveCommentToGame_ReturnsComment(
            Game game,
            Comment comment,
            [Frozen] Mock<IGameService> mockGameService,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            CommentService sut)
        {
            // Arrange
            mockGameService.Setup(x => x.GetGameByKey(It.IsAny<string>()))
                .Returns(game);

            mockUnitOfWork.Setup(x => x.Comments.Create(It.IsAny<Comment>()))
                .Returns(comment);

            // Act
            var actualComment = sut.LeaveCommentToGame(game.Key, comment);

            // Assert
            actualComment.Should().BeEquivalentTo(comment);

            mockGameService.Verify(x => x.GetGameByKey(It.IsAny<string>()), Times.Once);
            mockUnitOfWork.Verify(x => x.Comments.Create(It.IsAny<Comment>()), Times.Once);
            mockUnitOfWork.Verify(x => x.Commit(), Times.Once);
        }
    }
}
