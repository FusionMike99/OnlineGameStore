﻿using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Services;
using OnlineGameStore.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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
                    It.IsAny<string[]>()))
                .Returns(comments);

            // Act
            var actualComments = sut.GetAllCommentsByGameKey(gameKey);

            // Assert
            actualComments.Should().BeEquivalentTo(comments);

            mockUnitOfWork.Verify(x => x.Comments.GetMany(
                It.IsAny<Expression<Func<Comment, bool>>>(),
                It.IsAny<string[]>()),
                Times.Once);
        }
    }
}
