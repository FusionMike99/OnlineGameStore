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

namespace OnlineGameStore.Tests.Services.ShipperServiceTests
{
    public class ShipperServiceTests
    {
        [Theory]
        [AutoMoqData]
        public async Task ShipperService_GetAllShippers_ReturnsShippers(
            List<ShipperModel> shippers,
            [Frozen] Mock<IShipperRepository> shipperRepositoryMock,
            ShipperService sut)
        {
            // Arrange
            shipperRepositoryMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(shippers);

            // Act
            var actualShippers = await sut.GetAllShippersAsync();

            // Assert
            actualShippers.Should().BeEquivalentTo(shippers);

            shipperRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once);
        }
    }
}