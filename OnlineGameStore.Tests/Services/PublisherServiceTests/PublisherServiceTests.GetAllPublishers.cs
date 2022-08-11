using System.Collections.Generic;
using System.Threading.Tasks;
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
            var actualPublishers = await sut.GetAllPublishers();

            // Assert
            actualPublishers.Should().BeEquivalentTo(publishers);

            publisherRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once);
        }
    }
}