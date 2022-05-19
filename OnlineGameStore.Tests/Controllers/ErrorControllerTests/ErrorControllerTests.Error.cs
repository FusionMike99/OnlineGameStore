using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineGameStore.MVC.Controllers;
using OnlineGameStore.MVC.Models;
using OnlineGameStore.Tests.Helpers;
using Xunit;

namespace OnlineGameStore.Tests.Controllers
{
    public class ErrorControllerTests
    {
        [Theory]
        [AutoMoqData]
        public void Error_ReturnsViewResult(
            HttpContext httpContext,
            ErrorController sut)
        {
            // Arrange
            sut.ControllerContext.HttpContext = httpContext;

            // Act
            var result = sut.Error();

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<ErrorViewModel>();
        }
        
        [Theory]
        [AutoMoqData]
        public void Error_WithStatusCode_ReturnsViewResult(
            string statusCode,
            HttpContext httpContext,
            ErrorController sut)
        {
            // Arrange
            sut.ControllerContext.HttpContext = httpContext;

            // Act
            var result = sut.Error(statusCode);

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<ErrorViewModel>()
                .Which.StatusCode.Should().BeEquivalentTo(statusCode);
        }
    }
}