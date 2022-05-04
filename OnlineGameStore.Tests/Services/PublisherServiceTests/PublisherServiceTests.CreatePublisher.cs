using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using OnlineGameStore.BLL.Entities;
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
        public void PublisherService_CreatePublisher_ReturnsPublisher(
            Publisher publisher,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            PublisherService sut)
        {
            // Arrange
            mockUnitOfWork.Setup(x => x.Publishers.Create(It.IsAny<Publisher>()))
                .Returns(publisher);

            // Act
            var actualGame = sut.CreatePublisher(publisher);

            // Assert
            actualGame.Should().BeEquivalentTo(publisher);

            mockUnitOfWork.Verify(x => x.Publishers.Create(It.IsAny<Publisher>()), Times.Once);
            mockUnitOfWork.Verify(x => x.Commit(), Times.Once);
        }
    }
}
