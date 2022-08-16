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
        public async Task PublisherService_CreatePublisher_ReturnsPublisher(
            PublisherModel publisher,
            [Frozen] Mock<IPublisherRepository> publisherRepositoryMock,
            PublisherService sut)
        {
            // Arrange
            publisherRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<PublisherModel>()));

            // Act
            var actualGame = await sut.CreatePublisherAsync(publisher);

            // Assert
            actualGame.Should().BeEquivalentTo(publisher);

            publisherRepositoryMock.Verify(x => x.CreateAsync(It.IsAny<PublisherModel>()), Times.Once);
        }
    }
}