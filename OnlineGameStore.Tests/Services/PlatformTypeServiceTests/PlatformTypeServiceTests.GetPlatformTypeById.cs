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
        public async Task PlatformTypeService_GetPlatformTypeById_ReturnsPlatformType(
            PlatformTypeModel platformType,
            [Frozen] Mock<IPlatformTypeRepository> platformTypeRepositoryMock,
            PlatformTypeService sut)
        {
            // Arrange
            platformTypeRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>(),
                    It.IsAny<string[]>()))
                .ReturnsAsync(platformType);

            // Act
            var actualPlatformType = await sut.GetPlatformTypeByIdAsync(platformType.Id);

            // Assert
            actualPlatformType.Should().BeEquivalentTo(platformType);

            platformTypeRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<bool>(),
                It.IsAny<string[]>()), Times.Once);
        }
    }
}