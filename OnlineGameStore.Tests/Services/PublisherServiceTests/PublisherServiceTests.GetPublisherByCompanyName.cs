using System;
using System.Linq.Expressions;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Entities.Northwind;
using OnlineGameStore.BLL.Repositories;
using OnlineGameStore.BLL.Services;
using OnlineGameStore.Tests.Helpers;
using Xunit;

namespace OnlineGameStore.Tests.Services
{
    public partial class PublisherServiceTests
    {
        [Theory]
        [AutoMoqData]
        public void PublisherService_GetPublisherByCompanyName_ReturnsGameStorePublisher(
            Publisher publisher,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            PublisherService sut)
        {
            // Arrange
            mockUnitOfWork
                .Setup(m => m.Publishers.GetSingle(
                    It.IsAny<Expression<Func<Publisher, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<string[]>()))
                .Returns(publisher);

            // Act
            var actualPublisher = sut.GetPublisherByCompanyName(publisher.CompanyName);

            // Assert
            actualPublisher.Should().BeEquivalentTo(publisher);

            mockUnitOfWork.Verify(x => x.Publishers.GetSingle(
                    It.IsAny<Expression<Func<Publisher, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<string[]>()),
                Times.Once);
        }
        
        [Theory]
        [InlineAutoMoqData(null)]
        public void PublisherService_GetPublisherByCompanyName_ReturnsNorthwindPublisher(
            Publisher publisher,
            NorthwindSupplier supplier,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            [Frozen] Mock<INorthwindUnitOfWork> mockNorthwindUnitOfWork,
            PublisherService sut)
        {
            // Arrange
            mockUnitOfWork.Setup(m => m.Publishers.GetSingle(
                    It.IsAny<Expression<Func<Publisher, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<string[]>()))
                .Returns(publisher);

            mockNorthwindUnitOfWork.Setup(m => m.Suppliers.GetFirst(
                    It.IsAny<Expression<Func<NorthwindSupplier, bool>>>()))
                .Returns(supplier);

            // Act
            var actualPublisher = sut.GetPublisherByCompanyName(supplier.CompanyName);

            // Assert
            actualPublisher.CompanyName.Should().Be(supplier.CompanyName);

            mockUnitOfWork.Verify(x => x.Publishers.GetSingle(
                    It.IsAny<Expression<Func<Publisher, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<string[]>()),
                Times.Once);

            mockNorthwindUnitOfWork.Verify(x => x.Suppliers.GetFirst(
                    It.IsAny<Expression<Func<NorthwindSupplier, bool>>>()),
                Times.Once);
        }
    }
}