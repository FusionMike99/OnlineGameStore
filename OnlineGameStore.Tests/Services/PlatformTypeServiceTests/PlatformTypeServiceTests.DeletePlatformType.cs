using System;
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
            platformTypeRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(platformType);

            platformTypeRepositoryMock.Setup(x => x.DeleteAsync(It.IsAny<PlatformTypeModel>()));

            // Act
            await sut.DeletePlatformTypeAsync(platformType.Id);

            // Assert
            platformTypeRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
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
            platformTypeRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(platformType);

            // Act
            Func<Task> actual = async () => await sut.DeletePlatformTypeAsync(platformTypeId);

            // Assert
            await actual.Should().ThrowAsync<InvalidOperationException>();

            platformTypeRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
        }
    }
}