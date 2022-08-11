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
        public async Task PublisherService_GetPublisherByCompanyName_ReturnsGameStorePublisher(
            PublisherModel publisher,
            [Frozen] Mock<IPublisherRepository> publisherRepositoryMock,
            PublisherService sut)
        {
            // Arrange
            publisherRepositoryMock.Setup(x => x.GetByNameAsync(It.IsAny<string>(), It.IsAny<bool>()))
                .ReturnsAsync(publisher);

            // Act
            var actualPublisher = await sut.GetPublisherByCompanyName(publisher.CompanyName);

            // Assert
            actualPublisher.Should().BeEquivalentTo(publisher);

            publisherRepositoryMock.Verify(x => x.GetByNameAsync(It.IsAny<string>(), It.IsAny<bool>()),
                Times.Once);
        }
    }
}