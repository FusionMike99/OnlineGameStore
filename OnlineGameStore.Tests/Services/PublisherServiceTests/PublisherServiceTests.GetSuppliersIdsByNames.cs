using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
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
        public async Task PublisherService_GetSuppliersIdsByNames_ReturnsIds(
            List<string> suppliers,
            string[] names,
            [Frozen] Mock<IPublisherRepository> publisherRepositoryMock,
            PublisherService sut)
        {
            // Arrange
            publisherRepositoryMock.Setup(x => x.GetSuppliersIdsByNamesAsync(names))
                .ReturnsAsync(suppliers);

            // Act
            var actualPublishers = await sut.GetSuppliersIdsByNamesAsync(names);

            // Assert
            actualPublishers.Should().BeEquivalentTo(suppliers);

            publisherRepositoryMock.Verify(x => x.GetSuppliersIdsByNamesAsync(names), Times.Once);
        }
    }
}