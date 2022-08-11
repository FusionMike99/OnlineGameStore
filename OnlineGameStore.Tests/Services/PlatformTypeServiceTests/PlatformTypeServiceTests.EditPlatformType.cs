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
        public async Task PlatformTypeService_EditPlatformType_ReturnsPlatformType(
            PlatformTypeModel platformType,
            [Frozen] Mock<IPlatformTypeRepository> platformTypeRepositoryMock,
            PlatformTypeService sut)
        {
            // Arrange
            platformTypeRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<PlatformTypeModel>()));

            // Act
            var actualPlatformType = await sut.EditPlatformType(platformType);

            // Assert
            actualPlatformType.Should().BeEquivalentTo(platformType);

            platformTypeRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<PlatformTypeModel>()), Times.Once);
        }
    }
}