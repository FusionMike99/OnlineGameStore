using System.Collections.Generic;
using System.Linq;
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
        public async Task CommentService_GetAllCommentsByGameKey_ReturnsComments(
            string gameKey,
            List<CommentModel> comments,
            [Frozen] Mock<ICommentRepository> commentRepositoryMock,
            CommentService sut)
        {
            // Arrange
            commentRepositoryMock.Setup(x => x.GetAllByGameKeyAsync(It.IsAny<string>(), It.IsAny<bool>(),
                    It.IsAny<string[]>()))
                .ReturnsAsync(comments);

            var expectedComments = comments.Where(c => !c.ReplyToId.HasValue).ToList();

            // Act
            var actualComments = await sut.GetAllCommentsByGameKey(gameKey);

            // Assert
            actualComments.Should().BeEquivalentTo(expectedComments);

            commentRepositoryMock.Verify(x => x.GetAllByGameKeyAsync(It.IsAny<string>(), It.IsAny<bool>(),
                It.IsAny<string[]>()), Times.Once);
        }
    }
}