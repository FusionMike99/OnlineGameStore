using System;
using System.Linq.Expressions;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Repositories.GameStore;
using OnlineGameStore.BLL.Services;
using OnlineGameStore.Tests.Helpers;
using Xunit;

namespace OnlineGameStore.Tests.Services
{
    public partial class PlatformTypeServiceTests
    {
        [Theory]
        [AutoMoqData]
        public void PlatformTypeService_GetPlatformTypeById_ReturnsPlatformType(
            PlatformType platformType,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            PlatformTypeService sut)
        {
            // Arrange
            mockUnitOfWork
                .Setup(m => m.PlatformTypes.GetSingle(
                    It.IsAny<Expression<Func<PlatformType, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<string[]>()))
                .Returns(platformType);

            // Act
            var actualPlatformType = sut.GetPlatformTypeById(platformType.Id);

            // Assert
            actualPlatformType.Should().BeEquivalentTo(platformType);

            mockUnitOfWork.Verify(x => x.PlatformTypes.GetSingle(
                    It.IsAny<Expression<Func<PlatformType, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<string[]>()),
                Times.Once);
        }
    }
}