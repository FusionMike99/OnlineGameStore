using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using OnlineGameStore.MVC.Controllers;
using OnlineGameStore.Tests.Helpers;
using System;
using Xunit;

namespace OnlineGameStore.Tests.Controllers
{
    public partial class ErrorControllerTests
    {
        [Theory]
        [AutoMoqData]
        public void ErrorLocalDevelopment_ReturnsObjectResult_WhenEnvironmentIsNotDevelopment(
            ProblemDetails problemDetails,
            ExceptionHandlerFeature exceptionHandlerFeature,
            [Frozen] Mock<IFeatureCollection> mockFeatureCollection,
            [Frozen] Mock<HttpContext> mockHttpContext,
            [Frozen] Mock<ProblemDetailsFactory> mockProblemDetailsFactory,
            [Frozen] Mock<IWebHostEnvironment> mockWebHostEnvironment,
            ErrorController sut)
        {
            // Arrange
            mockWebHostEnvironment.SetupGet(x => x.EnvironmentName)
                .Returns("Development");

            mockProblemDetailsFactory
                .Setup(x => x.CreateProblemDetails(
                    It.IsAny<HttpContext>(),
                    It.IsAny<int?>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()))
                .Returns(problemDetails);

            mockFeatureCollection.Setup(p => p.Get<IExceptionHandlerFeature>())
                .Returns(exceptionHandlerFeature);

            mockHttpContext.Setup(x => x.Features)
                .Returns(mockFeatureCollection.Object);

            sut.ControllerContext.HttpContext = mockHttpContext.Object;

            sut.ProblemDetailsFactory = mockProblemDetailsFactory.Object;

            // Act
            var result = sut.ErrorLocalDevelopment(mockWebHostEnvironment.Object);

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
            mockFeatureCollection.Verify(x => x.Get<IExceptionHandlerFeature>(), Times.Once);
            mockHttpContext.Verify(x => x.Features, Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public void ErrorLocalDevelopment_ThrowsInvalidOperationException_WhenEnvironmentIsDevelopment(
            [Frozen] Mock<IWebHostEnvironment> mockWebHostEnvironment,
            ErrorController sut)
        {
            // Act
            Action actual = () => sut.ErrorLocalDevelopment(mockWebHostEnvironment.Object);

            // Assert
            actual.Should().Throw<InvalidOperationException>();
        }
    }
}
