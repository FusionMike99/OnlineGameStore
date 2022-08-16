using System.Collections.Generic;
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
        public async Task PlatformTypeService_GetAllPlatformTypes_ReturnsPlatformTypes(
            List<PlatformTypeModel> platformTypes,
            [Frozen] Mock<IPlatformTypeRepository> platformTypeRepositoryMock,
            PlatformTypeService sut)
        {
            // Arrange
            platformTypeRepositoryMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(platformTypes);

            // Act
            var actualPlatformTypes = await sut.GetAllPlatformTypesAsync();

            // Assert
            actualPlatformTypes.Should().BeEquivalentTo(platformTypes);

            platformTypeRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once);
        }
    }
}