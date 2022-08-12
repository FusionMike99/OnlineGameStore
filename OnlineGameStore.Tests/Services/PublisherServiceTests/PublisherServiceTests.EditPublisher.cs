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
        public async Task PublisherService_EditPublisher_ReturnsPublisher(
            DatabaseEntity databaseEntity,
            PublisherModel publisher,
            [Frozen] Mock<IPublisherRepository> publisherRepositoryMock,
            PublisherService sut)
        {
            // Arrange
            publisher.DatabaseEntity = databaseEntity;
            
            publisherRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<PublisherModel>()));

            // Act
            var actualPublisher = await sut.EditPublisherAsync(publisher);

            // Assert
            actualPublisher.Should().BeEquivalentTo(publisher);

            publisherRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<PublisherModel>()), Times.Once);
        }
        
        [Theory]
        [InlineAutoMoqData(DatabaseEntity.Northwind)]
        public void PublisherService_EditPublisher_ThrowsInvalidOperationExceptionWhenNorthwindDatabase(
            DatabaseEntity databaseEntity,
            PublisherModel publisher,
            PublisherService sut)
        {
            // Arrange
            publisher.DatabaseEntity = databaseEntity;

            // Act
            Func<Task> actual = async () => await sut.EditPublisherAsync(publisher);

            // Assert
            actual.Should().ThrowAsync<InvalidOperationException>();
        }
    }
}