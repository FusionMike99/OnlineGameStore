using System;
using System.Linq.Expressions;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Enums;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Services;
using OnlineGameStore.Tests.Helpers;
using Xunit;

namespace OnlineGameStore.Tests.Services
{
    public partial class PublisherServiceTests
    {
        [Theory]
        [InlineAutoMoqData(DatabaseEntity.GameStore)]
        public void PublisherService_EditPublisher_ReturnsPublisher(
            DatabaseEntity databaseEntity,
            Publisher publisher,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            PublisherService sut)
        {
            // Arrange
            publisher.DatabaseEntity = databaseEntity;
            
            mockUnitOfWork.Setup(x => x.Publishers.Update(It.IsAny<Publisher>(),
                    It.IsAny<Expression<Func<Publisher,bool>>>()))
                .Returns(publisher);

            // Act
            var actualPublisher = sut.EditPublisher(publisher.CompanyName, publisher);

            // Assert
            actualPublisher.Should().BeEquivalentTo(publisher);

            mockUnitOfWork.Verify(x => x.Publishers.Update(It.IsAny<Publisher>(),
                It.IsAny<Expression<Func<Publisher,bool>>>()), Times.Once);
            mockUnitOfWork.Verify(x => x.Commit(), Times.Once);
        }
        
        [Theory]
        [InlineAutoMoqData(DatabaseEntity.Northwind)]
        public void PublisherService_EditPublisher_ThrowsInvalidOperationExceptionWhenNorthwindDatabase(
            DatabaseEntity databaseEntity,
            Publisher publisher,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            PublisherService sut)
        {
            // Arrange
            publisher.DatabaseEntity = databaseEntity;
            
            mockUnitOfWork
                .Setup(m => m.Publishers.GetSingle(
                    It.IsAny<Expression<Func<Publisher, bool>>>(),
                    It.IsAny<bool>()))
                .Returns(publisher);

            // Act
            Action actual = () => sut.EditPublisher(publisher.CompanyName, publisher);

            // Assert
            actual.Should().Throw<InvalidOperationException>();
        }
    }
}