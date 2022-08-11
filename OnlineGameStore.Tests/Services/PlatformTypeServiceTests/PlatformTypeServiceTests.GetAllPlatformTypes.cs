﻿using System.Collections.Generic;
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
        public async Task PlatformTypeService_GetAllPlatformTypes_ReturnsPlatformTypes(
            List<PlatformTypeModel> platformTypes,
            [Frozen] Mock<IPlatformTypeRepository> platformTypeRepositoryMock,
            PlatformTypeService sut)
        {
            // Arrange
            platformTypeRepositoryMock.Setup(x => x.GetAllAsync(It.IsAny<bool>()))
                .ReturnsAsync(platformTypes);

            // Act
            var actualPlatformTypes = await sut.GetAllPlatformTypes();

            // Assert
            actualPlatformTypes.Should().BeEquivalentTo(platformTypes);

            platformTypeRepositoryMock.Verify(x => x.GetAllAsync(It.IsAny<bool>()), Times.Once);
        }
    }
}