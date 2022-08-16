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
        public async Task PublisherService_GetPublisherByCompanyName_ReturnsGameStorePublisher(
            PublisherModel publisher,
            [Frozen] Mock<IPublisherRepository> publisherRepositoryMock,
            PublisherService sut)
        {
            // Arrange
            publisherRepositoryMock.Setup(x => x.GetByNameAsync(It.IsAny<string>(), It.IsAny<bool>()))
                .ReturnsAsync(publisher);

            // Act
            var actualPublisher = await sut.GetPublisherByCompanyNameAsync(publisher.CompanyName);

            // Assert
            actualPublisher.Should().BeEquivalentTo(publisher);

            publisherRepositoryMock.Verify(x => x.GetByNameAsync(It.IsAny<string>(), It.IsAny<bool>()),
                Times.Once);
        }
    }
}