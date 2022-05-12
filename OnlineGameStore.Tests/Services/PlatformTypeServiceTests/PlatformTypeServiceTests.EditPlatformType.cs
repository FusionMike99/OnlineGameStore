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
    public partial class PlatformTypeServiceTests
    {
        [Theory]
        [AutoMoqData]
        public void PlatformTypeService_EditPlatformType_ReturnsPlatformType(
            PlatformType platformType,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            PlatformTypeService sut)
        {
            // Arrange
            mockUnitOfWork.Setup(x => x.PlatformTypes.Update(It.IsAny<PlatformType>()))
                .Returns(platformType);

            // Act
            var actualPlatformType = sut.EditPlatformType(platformType);

            // Assert
            actualPlatformType.Should().BeEquivalentTo(platformType);

            mockUnitOfWork.Verify(x => x.PlatformTypes.Update(It.IsAny<PlatformType>()), Times.Once);
            mockUnitOfWork.Verify(x => x.Commit(), Times.Once);
        }
    }
}