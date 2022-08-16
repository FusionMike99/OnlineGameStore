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