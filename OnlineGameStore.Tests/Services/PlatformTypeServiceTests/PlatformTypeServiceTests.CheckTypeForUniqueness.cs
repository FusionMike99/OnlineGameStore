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
    public partial class PlatformTypeServiceTests
    {
        [Theory]
        [AutoMoqData]
        public async Task CheckTypeForUniqueness_ReturnsTrue_WhenPlatformTypeIsNotNullAndIdIsNotSame(
            PlatformTypeModel platformType,
            Guid id,
            [Frozen] Mock<IPlatformTypeRepository> platformTypeRepositoryMock,
            PlatformTypeService sut)
        {
            // Arrange
            platformTypeRepositoryMock.Setup(x => x.GetByTypeAsync(It.IsAny<string>(),
                    It.IsAny<bool>()))
                .ReturnsAsync(platformType);

            // Act
            var actualResult = await sut.CheckTypeForUniqueAsync(id, platformType.Type);

            // Assert
            actualResult.Should().BeTrue();

            platformTypeRepositoryMock.Verify(x => x.GetByTypeAsync(It.IsAny<string>(),
                It.IsAny<bool>()), Times.Once);
        }

        [Theory]
        [InlineAutoMoqData(null)]
        public async Task CheckTypeForUniqueness_ReturnsFalse_WhenPlatformTypeIsNull(
            PlatformTypeModel platformType,
            Guid id,
            string type,
            [Frozen] Mock<IPlatformTypeRepository> platformTypeRepositoryMock,
            PlatformTypeService sut)
        {
            // Arrange
            platformTypeRepositoryMock.Setup(x => x.GetByTypeAsync(It.IsAny<string>(),
                    It.IsAny<bool>()))
                .ReturnsAsync(platformType);

            // Act
            var actualResult = await sut.CheckTypeForUniqueAsync(id, type);

            // Assert
            actualResult.Should().BeFalse();

            platformTypeRepositoryMock.Verify(x => x.GetByTypeAsync(It.IsAny<string>(),
                It.IsAny<bool>()), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public async Task CheckTypeForUniqueness_ReturnsFalse_WhenIdIsSame(
            PlatformTypeModel platformType,
            [Frozen] Mock<IPlatformTypeRepository> platformTypeRepositoryMock,
            PlatformTypeService sut)
        {
            // Arrange
            platformTypeRepositoryMock.Setup(x => x.GetByTypeAsync(It.IsAny<string>(),
                    It.IsAny<bool>()))
                .ReturnsAsync(platformType);

            // Act
            var actualResult = await sut.CheckTypeForUniqueAsync(platformType.Id, platformType.Type);

            // Assert
            actualResult.Should().BeFalse();

            platformTypeRepositoryMock.Verify(x => x.GetByTypeAsync(It.IsAny<string>(),
                It.IsAny<bool>()), Times.Once);
        }
    }
}