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
    public partial class PlatformTypeServiceTests
    {
        [Theory]
        [AutoMoqData]
        public async Task PlatformTypeService_GetPlatformTypesIdsByNames_ReturnsIds(
            List<string> platformTypes,
            List<string> types,
            [Frozen] Mock<IPlatformTypeRepository> platformTypeRepositoryMock,
            PlatformTypeService sut)
        {
            // Arrange
            platformTypeRepositoryMock.Setup(x => x.GetIdsByTypesAsync(types))
                .ReturnsAsync(platformTypes);

            // Act
            var actualPlatformTypes = await sut.GetPlatformTypesIdsByNames(types);

            // Assert
            actualPlatformTypes.Should().BeEquivalentTo(platformTypes);

            platformTypeRepositoryMock.Verify(x => x.GetIdsByTypesAsync(types), Times.Once);
        }
    }
}