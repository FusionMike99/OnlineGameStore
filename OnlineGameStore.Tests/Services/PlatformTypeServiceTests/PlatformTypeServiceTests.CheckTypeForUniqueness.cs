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
        public void CheckTypeForUniqueness_ReturnsTrue_WhenPlatformTypeIsNotNullAndIdIsNotSame(
            PlatformType platformType,
            int id,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            PlatformTypeService sut)
        {
            // Arrange
            mockUnitOfWork
                .Setup(m => m.PlatformTypes.GetSingle(
                    It.IsAny<Func<PlatformType, bool>>(),
                    It.IsAny<bool>(),
                    It.IsAny<string[]>()))
                .Returns(platformType);

            // Act
            var actualResult = sut.CheckTypeForUniqueness(id, platformType.Type);

            // Assert
            actualResult.Should().BeTrue();

            mockUnitOfWork.Verify(x => x.PlatformTypes.GetSingle(
                It.IsAny<Func<PlatformType, bool>>(),
                It.IsAny<bool>(),
                It.IsAny<string[]>()),
                Times.Once);
        }

        [Theory]
        [InlineAutoMoqData(null)]
        public void CheckTypeForUniqueness_ReturnsFalse_WhenPlatformTypeIsNull(
            PlatformType platformType,
            int id,
            string type,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            PlatformTypeService sut)
        {
            // Arrange
            mockUnitOfWork
                .Setup(m => m.PlatformTypes.GetSingle(
                    It.IsAny<Func<PlatformType, bool>>(),
                    It.IsAny<bool>(),
                    It.IsAny<string[]>()))
                .Returns(platformType);

            // Act
            var actualResult = sut.CheckTypeForUniqueness(id, type);

            // Assert
            actualResult.Should().BeFalse();

            mockUnitOfWork.Verify(x => x.PlatformTypes.GetSingle(
                It.IsAny<Func<PlatformType, bool>>(),
                It.IsAny<bool>(),
                It.IsAny<string[]>()),
                Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public void CheckTypeForUniqueness_ReturnsFalse_WhenIdIsSame(
            PlatformType platformType,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            PlatformTypeService sut)
        {
            // Arrange
            mockUnitOfWork
                .Setup(m => m.PlatformTypes.GetSingle(
                    It.IsAny<Func<PlatformType, bool>>(),
                    It.IsAny<bool>(),
                    It.IsAny<string[]>()))
                .Returns(platformType);

            // Act
            var actualResult = sut.CheckTypeForUniqueness(platformType.Id, platformType.Type);

            // Assert
            actualResult.Should().BeFalse();

            mockUnitOfWork.Verify(x => x.PlatformTypes.GetSingle(
                It.IsAny<Func<PlatformType, bool>>(),
                It.IsAny<bool>(),
                It.IsAny<string[]>()),
                Times.Once);
        }
    }
}
