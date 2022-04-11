using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using OnlineGameStore.MVC.Controllers;
using OnlineGameStore.Tests.Helpers;
using Xunit;

namespace OnlineGameStore.Tests.Controllers
{
    public partial class ErrorControllerTests
    {
        [Theory]
        [AutoMoqData]
        public void Error_ReturnsObjectResult(
            ProblemDetails problemDetails,
            [Frozen] Mock<ProblemDetailsFactory> mockProblemDetailsFactory,
            ErrorController sut)
        {
            // Arrange
            mockProblemDetailsFactory
                .Setup(x => x.CreateProblemDetails(
                    It.IsAny<HttpContext>(),
                    It.IsAny<int?>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()))
                .Returns(problemDetails);

            sut.ProblemDetailsFactory = mockProblemDetailsFactory.Object;

            // Act
            var result = sut.Error();

            // Assert
            result.Should().BeOfType<ObjectResult>()
                .Which.Value.Should().BeAssignableTo<ProblemDetails>()
                .Which.Should().BeEquivalentTo(problemDetails);

            mockProblemDetailsFactory.Verify(x => x.CreateProblemDetails(
                It.IsAny<HttpContext>(),
                It.IsAny<int?>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()), Times.Once);
        }
    }
}
