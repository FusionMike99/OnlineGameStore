using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Services;
using OnlineGameStore.Tests.Helpers;
using System;
using Xunit;

namespace OnlineGameStore.Tests.Services
{
    public partial class PlatformTypeServiceTests
    {
        [Theory]
        [AutoMoqData]
        public void PlatformTypeService_DeletePlatformType_DeletesPlatformType(
            PlatformType platformType,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            PlatformTypeService sut)
        {
            // Arrange
            mockUnitOfWork
                .Setup(m => m.PlatformTypes.GetSingle(
                    It.IsAny<Func<PlatformType, bool>>(),
                    It.IsAny<bool>()))
                .Returns(platformType);

            mockUnitOfWork.Setup(x => x.PlatformTypes.Delete(It.IsAny<PlatformType>()));

            // Act
            sut.DeletePlatformType(platformType.Id);

            // Assert
            mockUnitOfWork.Verify(x => x.PlatformTypes.GetSingle(
                It.IsAny<Func<PlatformType, bool>>(),
                It.IsAny<bool>()),
                Times.Once);
            mockUnitOfWork.Verify(x => x.PlatformTypes.Delete(
                It.Is<PlatformType>(g => g.Type == platformType.Type && g.Id == platformType.Id)),
                Times.Once);
            mockUnitOfWork.Verify(x => x.Commit(), Times.Once);
        }

        [Theory]
        [InlineAutoMoqData(null)]
        public void PlatformTypeService_DeletPlatformType_ThrowsInvalidOperationExceptionWithNullEntity(
            PlatformType platformType,
            int platformTypeId,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            PlatformTypeService sut)
        {
            // Arrange
            mockUnitOfWork
                .Setup(m => m.PlatformTypes.GetSingle(
                    It.IsAny<Func<PlatformType, bool>>(),
                    It.IsAny<bool>()))
                .Returns(platformType);

            // Act
            Action actual = () => sut.DeletePlatformType(platformTypeId);

            // Assert
            actual.Should().Throw<InvalidOperationException>();

            mockUnitOfWork.Verify(x => x.PlatformTypes.GetSingle(
                It.IsAny<Func<PlatformType, bool>>(),
                It.IsAny<bool>(),
                It.IsAny<string[]>()),
                Times.Once);
        }
    }
}
