using System;
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
        public async Task CheckCompanyNameForUniqueness_ReturnsTrue_WhenPublisherIsNotNullAndIdIsNotSame(
            PublisherModel publisher,
            Guid id,
            [Frozen] Mock<IPublisherRepository> publisherRepositoryMock,
            PublisherService sut)
        {
            // Arrange
            publisherRepositoryMock.Setup(x => x.GetByNameAsync(It.IsAny<string>(), It.IsAny<bool>()))
                .ReturnsAsync(publisher);

            // Act
            var actualResult = await sut.CheckCompanyNameForUniqueAsync(id, publisher.CompanyName);

            // Assert
            actualResult.Should().BeTrue();

            publisherRepositoryMock.Verify(x => x.GetByNameAsync(It.IsAny<string>(), It.IsAny<bool>()),
                Times.Once);
        }

        [Theory]
        [InlineAutoMoqData(null)]
        public async Task CheckCompanyNameForUniqueness_ReturnsFalse_WhenPublisherIsNull(
            PublisherModel publisher,
            Guid id,
            string companyName,
            [Frozen] Mock<IPublisherRepository> publisherRepositoryMock,
            PublisherService sut)
        {
            // Arrange
            publisherRepositoryMock.Setup(x => x.GetByNameAsync(It.IsAny<string>(), It.IsAny<bool>()))
                .ReturnsAsync(publisher);

            // Act
            var actualResult = await sut.CheckCompanyNameForUniqueAsync(id, companyName);

            // Assert
            actualResult.Should().BeFalse();

            publisherRepositoryMock.Verify(x => x.GetByNameAsync(It.IsAny<string>(), It.IsAny<bool>()),
                Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public async Task CheckCompanyNameForUniqueness_ReturnsFalse_WhenIdIsSame(
            PublisherModel publisher,
            [Frozen] Mock<IPublisherRepository> publisherRepositoryMock,
            PublisherService sut)
        {
            // Arrange
            publisherRepositoryMock.Setup(x => x.GetByNameAsync(It.IsAny<string>(), It.IsAny<bool>()))
                .ReturnsAsync(publisher);

            // Act
            var actualResult = await sut.CheckCompanyNameForUniqueAsync(publisher.Id, publisher.CompanyName);

            // Assert
            actualResult.Should().BeFalse();

            publisherRepositoryMock.Verify(x => x.GetByNameAsync(It.IsAny<string>(), It.IsAny<bool>()),
                Times.Once);
        }
    }
}