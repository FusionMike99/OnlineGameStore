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
        public async Task PublisherService_CreatePublisher_ReturnsPublisher(
            PublisherModel publisher,
            [Frozen] Mock<IPublisherRepository> publisherRepositoryMock,
            PublisherService sut)
        {
            // Arrange
            publisherRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<PublisherModel>()));

            // Act
            var actualGame = await sut.CreatePublisher(publisher);

            // Assert
            actualGame.Should().BeEquivalentTo(publisher);

            publisherRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<PublisherModel>()), Times.Once);
        }
    }
}