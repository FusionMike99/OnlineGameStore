using System;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using OnlineGameStore.BLL.Enums;
using OnlineGameStore.BLL.Models.General;
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
        public async Task PublisherService_DeletePublisher_DeletesPublisher(
            DatabaseEntity databaseEntity,
            PublisherModel publisher,
            [Frozen] Mock<IPublisherRepository> publisherRepositoryMock,
            PublisherService sut)
        {
            // Arrange
            publisher.DatabaseEntity = databaseEntity;
            
            publisherRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>()))
                .ReturnsAsync(publisher);

            publisherRepositoryMock.Setup(x => x.DeleteAsync(It.IsAny<PublisherModel>()));

            // Act
            await sut.DeletePublisher(publisher.Id);

            // Assert
            publisherRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>()),
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
            publisherRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>()))
                .ReturnsAsync(publisher);

            // Act
            Func<Task> actual = async () => await sut.DeletePublisher(publisherId);

            // Assert
            actual.Should().ThrowAsync<InvalidOperationException>();

            publisherRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>()),
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
            
            publisherRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>()))
                .ReturnsAsync(publisher);

            // Act
            Func<Task> actual = async () => await sut.DeletePublisher(publisher.Id);

            // Assert
            actual.Should().ThrowAsync<InvalidOperationException>();

            publisherRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>()),
                Times.Once);
        }
    }
}