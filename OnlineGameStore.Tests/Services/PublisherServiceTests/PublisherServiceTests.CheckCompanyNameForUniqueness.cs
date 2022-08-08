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
    public partial class PublisherServiceTests
    {
        [Theory]
        [AutoMoqData]
        public void CheckCompanyNameForUniqueness_ReturnsTrue_WhenPublisherIsNotNullAndIdIsNotSame(
            Publisher publisher,
            string id,
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
            var actualResult = sut.CheckCompanyNameForUnique(id, publisher.CompanyName);

            // Assert
            actualResult.Should().BeTrue();

            mockUnitOfWork.Verify(x => x.Publishers.GetSingle(
                    It.IsAny<Expression<Func<Publisher, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<string[]>()),
                Times.Once);
        }

        [Theory]
        [InlineAutoMoqData(null)]
        public void CheckCompanyNameForUniqueness_ReturnsFalse_WhenPublisherIsNull(
            Publisher publisher,
            string id,
            string companyName,
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
            var actualResult = sut.CheckCompanyNameForUnique(id, companyName);

            // Assert
            actualResult.Should().BeFalse();

            mockUnitOfWork.Verify(x => x.Publishers.GetSingle(
                    It.IsAny<Expression<Func<Publisher, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<string[]>()),
                Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public void CheckCompanyNameForUniqueness_ReturnsFalse_WhenIdIsSame(
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
            var actualResult = sut.CheckCompanyNameForUnique(publisher.Id.ToString(), publisher.CompanyName);

            // Assert
            actualResult.Should().BeFalse();

            mockUnitOfWork.Verify(x => x.Publishers.GetSingle(
                    It.IsAny<Expression<Func<Publisher, bool>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<string[]>()),
                Times.Once);
        }
    }
}