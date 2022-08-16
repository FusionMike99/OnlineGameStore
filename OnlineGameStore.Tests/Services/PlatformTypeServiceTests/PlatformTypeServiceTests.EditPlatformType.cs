﻿using System.Threading.Tasks;
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
        public async Task PlatformTypeService_EditPlatformType_ReturnsPlatformType(
            PlatformTypeModel platformType,
            [Frozen] Mock<IPlatformTypeRepository> platformTypeRepositoryMock,
            PlatformTypeService sut)
        {
            // Arrange
            platformTypeRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<PlatformTypeModel>()));

            // Act
            var actualPlatformType = await sut.EditPlatformTypeAsync(platformType);

            // Assert
            actualPlatformType.Should().BeEquivalentTo(platformType);

            platformTypeRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<PlatformTypeModel>()), Times.Once);
        }
    }
}