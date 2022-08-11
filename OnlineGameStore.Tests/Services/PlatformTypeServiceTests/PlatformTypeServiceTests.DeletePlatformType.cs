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
        public async Task PlatformTypeService_DeletePlatformType_DeletesPlatformType(
            PlatformTypeModel platformType,
            [Frozen] Mock<IPlatformTypeRepository> platformTypeRepositoryMock,
            PlatformTypeService sut)
        {
            // Arrange
            platformTypeRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>(),
                    It.IsAny<string[]>()))
                .ReturnsAsync(platformType);

            platformTypeRepositoryMock.Setup(x => x.DeleteAsync(It.IsAny<PlatformTypeModel>()));

            // Act
            await sut.DeletePlatformType(platformType.Id);

            // Assert
            platformTypeRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>(),
                It.IsAny<string[]>()), Times.Once);
            platformTypeRepositoryMock.Verify(x => x.DeleteAsync(It.IsAny<PlatformTypeModel>()),
                Times.Once);
        }

        [Theory]
        [InlineAutoMoqData(null)]
        public async Task PlatformTypeService_DeletePlatformType_ThrowsInvalidOperationExceptionWithNullEntity(
            PlatformTypeModel platformType,
            Guid platformTypeId,
            [Frozen] Mock<IPlatformTypeRepository> platformTypeRepositoryMock,
            PlatformTypeService sut)
        {
            // Arrange
            platformTypeRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>(),
                    It.IsAny<string[]>()))
                .ReturnsAsync(platformType);

            // Act
            Func<Task> actual = async () => await sut.DeletePlatformType(platformTypeId);

            // Assert
            await actual.Should().ThrowAsync<InvalidOperationException>();

            platformTypeRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>(),
                It.IsAny<string[]>()), Times.Once);
        }
    }
}