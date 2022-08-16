using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using OnlineGameStore.BLL.Services;
using OnlineGameStore.DAL.Abstractions.Interfaces;
using OnlineGameStore.DomainModels.Models.General;
using OnlineGameStore.Tests.Helpers;
using Xunit;

namespace OnlineGameStore.Tests.Services
{
    public partial class PublisherServiceTests
    {
        [Theory]
        [AutoMoqData]
        public async Task PublisherService_GetAllPublishers_ReturnsPublishers(
            List<PublisherModel> publishers,
            [Frozen] Mock<IPublisherRepository> publisherRepositoryMock,
            PublisherService sut)
        {
            // Arrange
            publisherRepositoryMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(publishers);

            // Act
            var actualPublishers = await sut.GetAllPublishersAsync();

            // Assert
            actualPublishers.Should().BeEquivalentTo(publishers);

            publisherRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once);
        }
    }
}