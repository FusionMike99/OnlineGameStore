﻿using System;
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
        public void PlatformTypeService_GetAllPlatformTypes_ReturnsPlatformTypes(
            List<PlatformType> platformTypes,
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

            // Act
            var actualPlatformTypes = sut.GetAllPlatformTypes();

            // Assert
            actualPlatformTypes.Should().BeEquivalentTo(platformTypes);

            mockUnitOfWork.Verify(x => x.PlatformTypes.GetMany(
                    It.IsAny<Expression<Func<PlatformType, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<Func<IQueryable<PlatformType>,IOrderedQueryable<PlatformType>>>(),
                    It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<string[]>()),
                Times.Once);
        }
    }
}