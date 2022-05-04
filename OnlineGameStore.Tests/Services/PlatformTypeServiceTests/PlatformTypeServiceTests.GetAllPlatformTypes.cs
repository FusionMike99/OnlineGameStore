using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Services;
using OnlineGameStore.Tests.Helpers;
using System;
using System.Collections.Generic;
using Xunit;

namespace OnlineGameStore.Tests.Services
{
    public partial class PlatformTypeServiceTests
    {
        [Theory]
        [AutoMoqData]
        public void PlatformTypeService_GetAllPlatformTypes_ReturnsPlatformTypes(
            IEnumerable<PlatformType> platformTypes,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            PlatformTypeService sut)
        {
            // Arrange
            mockUnitOfWork
                .Setup(m => m.PlatformTypes.GetMany(
                    It.IsAny<Func<PlatformType, bool>>(),
                    It.IsAny<bool>(),
                    It.IsAny<string[]>()))
                .Returns(platformTypes);

            // Act
            var actualPlatformTypes = sut.GetAllPlatformTypes();

            // Assert
            actualPlatformTypes.Should().BeEquivalentTo(platformTypes);

            mockUnitOfWork.Verify(x => x.PlatformTypes.GetMany(
                It.IsAny<Func<PlatformType, bool>>(),
                It.IsAny<bool>(),
                It.IsAny<string[]>()),
                Times.Once);
        }
    }
}
