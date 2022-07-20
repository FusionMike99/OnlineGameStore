using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using OnlineGameStore.BLL.Entities;
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
        public void PlatformTypeService_GetPlatformTypesIdsByNames_ReturnsIds(
            List<PlatformType> platformTypes,
            List<string> types,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            PlatformTypeService sut)
        {
            // Arrange
            mockUnitOfWork
                .Setup(m => m.PlatformTypes.GetMany(
                    It.IsAny<Expression<Func<PlatformType, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<Func<IQueryable<PlatformType>,IOrderedQueryable<PlatformType>>>(),
                    It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<string[]>()))
                .Returns(platformTypes);

            var expectedIds = platformTypes.Select(x => x.Id);

            // Act
            var actualPlatformTypes = sut.GetPlatformTypesIdsByNames(types);

            // Assert
            actualPlatformTypes.Should().BeEquivalentTo(expectedIds);

            mockUnitOfWork.Verify(x => x.PlatformTypes.GetMany(
                    It.IsAny<Expression<Func<PlatformType, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<Func<IQueryable<PlatformType>,IOrderedQueryable<PlatformType>>>(),
                    It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<string[]>()),
                Times.Once);
        }
    }
}