using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using OnlineGameStore.BLL.Services;
using OnlineGameStore.BLL.Services.Interfaces;
using OnlineGameStore.DAL.Abstractions.Interfaces;
using OnlineGameStore.DomainModels.Models.General;
using OnlineGameStore.Tests.Helpers;
using Xunit;

namespace OnlineGameStore.Tests.Services
{
    public partial class CommentServiceTests
    {
        [Theory]
        [AutoMoqData]
        public async Task CommentService_LeaveCommentToGame_ReturnsComment(
            GameModel game,
            CommentModel comment,
            [Frozen] Mock<IGameService> mockGameService,
            [Frozen] Mock<ICommentRepository> commentRepositoryMock,
            CommentService sut)
        {
            // Arrange
            mockGameService.Setup(x => x.GetGameByKeyAsync(It.IsAny<string>(),
                    It.IsAny<bool>()))
                .ReturnsAsync(game);

            commentRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<CommentModel>()));

            // Act
            var actualComment = await sut.LeaveCommentToGameAsync(game.Key, comment);

            // Assert
            actualComment.Should().BeEquivalentTo(comment);
            mockGameService.Verify(x => x.GetGameByKeyAsync(It.IsAny<string>(), It.IsAny<bool>()), Times.Once);
            commentRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<CommentModel>()), Times.Once);
        }
    }
}