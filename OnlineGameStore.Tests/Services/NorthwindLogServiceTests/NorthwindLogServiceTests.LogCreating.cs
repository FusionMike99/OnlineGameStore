using AutoFixture.Xunit2;
using Moq;
using OnlineGameStore.BLL.Models;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Services;
using OnlineGameStore.Tests.Helpers;
using Xunit;

namespace OnlineGameStore.Tests.Services.NorthwindLogServiceTests
{
    public partial class NorthwindLogServiceTests
    {
        [Theory]
        [AutoMoqData]
        public void NorthwindLogService_LogCreating(
            TestEntity testEntity,
            [Frozen] Mock<INorthwindUnitOfWork> mockNorthwindUnitOfWork,
            NorthwindLogService sut)
        {
            // Arrange
            mockNorthwindUnitOfWork.Setup(x => x.Logs.Create(It.IsAny<LogModel>()));

            // Act
            sut.LogCreating(testEntity);

            // Assert
            mockNorthwindUnitOfWork.Verify(x => x.Logs.Create(It.IsAny<LogModel>()), Times.Once);
        }
    }
}