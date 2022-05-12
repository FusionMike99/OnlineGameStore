using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using OnlineGameStore.MVC.Infrastructure;
using OnlineGameStore.Tests.Helpers;
using Xunit;

namespace OnlineGameStore.Tests.Infrastructure
{
    public class TrimmingModelBinderTests
    {
        [Theory]
        [InlineAutoMoqData("   text", "text")]
        [InlineAutoMoqData("text    ", "text")]
        [InlineAutoMoqData("    text    ", "text")]
        [InlineAutoMoqData("    text1 text2    ", "text1 text2")]
        public void BindModelAsync_ReturnsCompletedTask_AndExpectedResult(
            string text,
            string expected,
            TrimmingModelBinder sut)
        {
            // Arrange
            var bindingContext = new DefaultModelBindingContext();

            var bindingSource = new BindingSource("", "", false, false);
            var routeValueDictionary = new RouteValueDictionary
            {
                { "TestValue", text }
            };

            bindingContext.ModelName = "TestValue";
            bindingContext.ValueProvider = new RouteValueProvider(bindingSource, routeValueDictionary);

            // Act
            var result = sut.BindModelAsync(bindingContext);

            // Assert
            result.Should().BeOfType<Task>()
                .Which.IsCompleted.Should().BeTrue();

            bindingContext.Result.IsModelSet.Should().BeTrue();
            bindingContext.Result.Model.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [AutoMoqData]
        public void BindModelAsync_ArgumentNullExceptionWithNullEntity(
            TrimmingModelBinder sut)
        {
            // Act
            Action actual = () => sut.BindModelAsync(null);

            // Assert
            actual.Should().Throw<ArgumentNullException>();
        }
    }
}