using System;
using System.Linq.Expressions;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Enums;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Repositories.GameStore;
using OnlineGameStore.BLL.Services;
using OnlineGameStore.Tests.Helpers;
using Xunit;

namespace OnlineGameStore.Tests.Services
{
    public partial class PublisherServiceTests
    {
        [Theory]
        [InlineAutoMoqData(DatabaseEntity.GameStore)]
        public void PublisherService_DeletePublisher_DeletesPublisher(
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

            mockUnitOfWork.Setup(x => x.Publishers.Delete(It.IsAny<Publisher>()));

            // Act
            sut.DeletePublisher(publisher.Id.ToString());

            // Assert
            mockUnitOfWork.Verify(x => x.Publishers.GetSingle(
                    It.IsAny<Expression<Func<Publisher, bool>>>(),
                    It.IsAny<bool>()),
                Times.Once);
            mockUnitOfWork.Verify(x => x.Publishers.Delete(
                    It.Is<Publisher>(p => p.CompanyName == publisher.CompanyName && p.Id == publisher.Id)),
                Times.Once);
            mockUnitOfWork.Verify(x => x.Commit(), Times.Once);
        }

        [Theory]
        [InlineAutoMoqData(null)]
        public void PublisherService_DeletePublisher_ThrowsInvalidOperationExceptionWithNullEntity(
            Publisher publisher,
            string publisherId,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            PublisherService sut)
        {
            // Arrange
            mockUnitOfWork
                .Setup(m => m.Publishers.GetSingle(
                    It.IsAny<Expression<Func<Publisher, bool>>>(),
                    It.IsAny<bool>()))
                .Returns(publisher);

            // Act
            Action actual = () => sut.DeletePublisher(publisherId);

            // Assert
            actual.Should().Throw<InvalidOperationException>();

            mockUnitOfWork.Verify(x => x.Publishers.GetSingle(
                    It.IsAny<Expression<Func<Publisher, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<string[]>()),
                Times.Once);
        }
        
        [Theory]
        [InlineAutoMoqData(DatabaseEntity.Northwind)]
        public void PublisherService_DeletePublisher_ThrowsInvalidOperationExceptionWhenNorthwindDatabase(
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
            Action actual = () => sut.DeletePublisher(publisher.Id.ToString());

            // Assert
            actual.Should().Throw<InvalidOperationException>();

            mockUnitOfWork.Verify(x => x.Publishers.GetSingle(
                    It.IsAny<Expression<Func<Publisher, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<string[]>()),
                Times.Once);
        }
    }
}