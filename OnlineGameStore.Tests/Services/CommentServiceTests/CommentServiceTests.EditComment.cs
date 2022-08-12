﻿using System.Threading.Tasks;
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
        public async Task CommentService_EditComment_ReturnsComment(
            CommentModel comment,
            [Frozen] Mock<ICommentRepository> commentRepositoryMock,
            CommentService sut)
        {
            // Arrange
            commentRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<CommentModel>()));

            // Act
            var actualComment = await sut.EditCommentAsync(comment);

            // Assert
            actualComment.Should().BeEquivalentTo(comment);

            commentRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<CommentModel>()), Times.Once);
        }
    }
}