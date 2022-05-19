using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineGameStore.BLL.Enums;
using OnlineGameStore.BLL.Services.Contracts;
using OnlineGameStore.MVC.Controllers;
using OnlineGameStore.MVC.Models;
using OnlineGameStore.Tests.Helpers;
using Xunit;

namespace OnlineGameStore.Tests.Controllers
{
    public class UserControllerTests
    {
        [Theory]
        [AutoMoqData]
        public void Ban_Get_ReturnsViewResult(
            string userName,
            string returnUrl,
            UserController sut)
        {
            // Act
            var result = sut.Ban(userName, returnUrl);

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<BanViewModel>();
        }

        [Theory]
        [InlineAutoMoqData(true)]
        public void Ban_Post_ReturnsRedirectResult_WhenModelIsValidAndReturnUrlIsNotEmptyAndLocal(
            string message,
            bool isLocalUrl,
            BanViewModel banViewModel,
            [Frozen] Mock<IUserService> mockUserService,
            [Frozen] Mock<IUrlHelper> mockUrlHelper,
            UserController sut)
        {
            // Arrange
            mockUserService.Setup(x => x.BanUser(It.IsAny<string>(),
                    It.IsAny<BanPeriod>()))
                .Returns(message);

            mockUrlHelper.Setup(x => x.IsLocalUrl(It.IsAny<string>()))
                .Returns(isLocalUrl);

            sut.Url = mockUrlHelper.Object;
            
            // Act
            var result = sut.Ban(banViewModel);

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Subject.Model.Should().BeAssignableTo<AfterBanViewModel>()
                .Subject.Message.Should().BeEquivalentTo(message);

            mockUserService.Verify(x => x.BanUser(It.IsAny<string>(),
                It.IsAny<BanPeriod>()),
                Times.Once);
            
            mockUrlHelper.Verify(x => x.IsLocalUrl(It.IsAny<string>()),
                Times.Once);
        }
        
        [Theory]
        [InlineAutoMoqData(true)]
        public void Ban_Post_ReturnsRedirectToActionResult_WhenModelIsValidAndReturnUrlIsEmpty(
            bool isLocalUrl,
            BanViewModel banViewModel,
            [Frozen] Mock<IUserService> mockUserService,
            [Frozen] Mock<IUrlHelper> mockUrlHelper,
            UserController sut)
        {
            // Arrange
            mockUserService.Setup(x => x.BanUser(It.IsAny<string>(),
                It.IsAny<BanPeriod>()));

            mockUrlHelper.Setup(x => x.IsLocalUrl(It.IsAny<string>()))
                .Returns(isLocalUrl);

            sut.Url = mockUrlHelper.Object;
            
            banViewModel.ReturnUrl = string.Empty;
            
            // Act
            var result = sut.Ban(banViewModel);

            // Assert
            result.Should().BeOfType<RedirectToActionResult>()
                .Subject.ActionName.Should().BeEquivalentTo(nameof(GameController.GetGames));

            mockUserService.Verify(x => x.BanUser(It.IsAny<string>(),
                    It.IsAny<BanPeriod>()),
                Times.Once);
            
            mockUrlHelper.Verify(x => x.IsLocalUrl(It.IsAny<string>()),
                Times.Never);
        }
        
        [Theory]
        [InlineAutoMoqData(false)]
        public void Ban_Post_ReturnsRedirectToActionResult_WhenModelIsValidAndReturnUrlIsNotLocal(
            bool isLocalUrl,
            BanViewModel banViewModel,
            [Frozen] Mock<IUserService> mockUserService,
            [Frozen] Mock<IUrlHelper> mockUrlHelper,
            UserController sut)
        {
            // Arrange
            mockUserService.Setup(x => x.BanUser(It.IsAny<string>(),
                It.IsAny<BanPeriod>()));

            mockUrlHelper.Setup(x => x.IsLocalUrl(It.IsAny<string>()))
                .Returns(isLocalUrl);

            sut.Url = mockUrlHelper.Object;
            
            // Act
            var result = sut.Ban(banViewModel);

            // Assert
            result.Should().BeOfType<RedirectToActionResult>()
                .Subject.ActionName.Should().BeEquivalentTo(nameof(GameController.GetGames));

            mockUserService.Verify(x => x.BanUser(It.IsAny<string>(),
                    It.IsAny<BanPeriod>()),
                Times.Once);
            
            mockUrlHelper.Verify(x => x.IsLocalUrl(It.IsAny<string>()),
                Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public void Ban_Post_ReturnsViewResult_WhenModelIsInvalid(
            BanViewModel banViewModel,
            UserController sut)
        {
            // Arrange
            sut.ModelState.AddModelError("UserName", "Required");

            // Act
            var result = sut.Ban(banViewModel);

            // Assert
            result.Should().BeOfType<ViewResult>()
                .Which.Model.Should().BeAssignableTo<BanViewModel>()
                .Which.Should().BeEquivalentTo(banViewModel);
        }
    }
}