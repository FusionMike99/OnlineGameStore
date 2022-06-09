using System;
using System.Collections.Generic;
using System.Linq;
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
    public partial class PublisherServiceTests
    {
        [Theory]
        [AutoMoqData]
        public void PublisherService_GetAllPublishers_ReturnsPublishers(
            IEnumerable<Publisher> publishers,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            PublisherService sut)
        {
            // Arrange
            mockUnitOfWork
                .Setup(m => m.Publishers.GetMany(
                    It.IsAny<Expression<Func<Publisher, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<Func<IQueryable<Publisher>,IOrderedQueryable<Publisher>>>(),
                    It.IsAny<int?>(),
                    It.IsAny<int?>(),
                    It.IsAny<string[]>()))
                .Returns(publishers);

            // Act
            var actualPublishers = sut.GetAllPublishers();

            // Assert
            actualPublishers.Should().BeEquivalentTo(publishers);

            mockUnitOfWork.Verify(x => x.Publishers.GetMany(
                    It.IsAny<Expression<Func<Publisher, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<Func<IQueryable<Publisher>,IOrderedQueryable<Publisher>>>(),
                    It.IsAny<int?>(),
                    It.IsAny<int?>(),
                    It.IsAny<string[]>()),
                Times.Once);
        }
    }
}