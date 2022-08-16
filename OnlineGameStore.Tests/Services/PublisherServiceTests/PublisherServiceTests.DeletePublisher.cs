using System;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using OnlineGameStore.BLL.Services;
using OnlineGameStore.DAL.Abstractions.Interfaces;
using OnlineGameStore.DomainModels.Enums;
using OnlineGameStore.DomainModels.Models.General;
using OnlineGameStore.Tests.Helpers;
using Xunit;

namespace OnlineGameStore.Tests.Services
{
    public partial class PublisherServiceTests
    {
        [Theory]
        [InlineAutoMoqData(DatabaseEntity.GameStore)]
        public async Task PublisherService_DeletePublisher_DeletesPublisher(
            DatabaseEntity databaseEntity,
            PublisherModel publisher,
            [Frozen] Mock<IPublisherRepository> publisherRepositoryMock,
            PublisherService sut)
        {
            // Arrange
            publisher.DatabaseEntity = databaseEntity;
            
            publisherRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(publisher);

            publisherRepositoryMock.Setup(x => x.DeleteAsync(It.IsAny<PublisherModel>()));

            // Act
            await sut.DeletePublisherAsync(publisher.Id);

            // Assert
            publisherRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<Guid>()),
                Times.Once);
            publisherRepositoryMock.Verify(x => x.DeleteAsync(It.IsAny<PublisherModel>()), Times.Once);
        }

        [Theory]
        [InlineAutoMoqData(null)]
        public void PublisherService_DeletePublisher_ThrowsInvalidOperationExceptionWithNullEntity(
            PublisherModel publisher,
            Guid publisherId,
            [Frozen] Mock<IPublisherRepository> publisherRepositoryMock,
            PublisherService sut)
        {
            // Arrange
            publisherRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(publisher);

            // Act
            Func<Task> actual = async () => await sut.DeletePublisherAsync(publisherId);

            // Assert
            actual.Should().ThrowAsync<InvalidOperationException>();

            publisherRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<Guid>()),
                Times.Once);
        }
        
        [Theory]
        [InlineAutoMoqData(DatabaseEntity.Northwind)]
        public void PublisherService_DeletePublisher_ThrowsInvalidOperationExceptionWhenNorthwindDatabase(
            DatabaseEntity databaseEntity,
            PublisherModel publisher,
            [Frozen] Mock<IPublisherRepository> publisherRepositoryMock,
            PublisherService sut)
        {
            // Arrange
            publisher.DatabaseEntity = databaseEntity;
            
            publisherRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(publisher);

            // Act
            Func<Task> actual = async () => await sut.DeletePublisherAsync(publisher.Id);

            // Assert
            actual.Should().ThrowAsync<InvalidOperationException>();

            publisherRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<Guid>()),
                Times.Once);
        }
    }
}