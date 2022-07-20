using System;
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
    public partial class GenreServiceTests
    {
        [Theory]
        [AutoMoqData]
        public void GenreService_EditGenre_ReturnsGenre(
            Genre genre,
            [Frozen] Mock<IUnitOfWork> mockUnitOfWork,
            GenreService sut)
        {
            // Arrange
            mockUnitOfWork.Setup(x => x.Genres.Update(It.IsAny<Genre>(),
                    It.IsAny<Expression<Func<Genre,bool>>>()))
                .Returns(genre);

            // Act
            var actualGenre = sut.EditGenre(genre);

            // Assert
            actualGenre.Should().BeEquivalentTo(genre);

            mockUnitOfWork.Verify(x => x.Genres.Update(It.IsAny<Genre>(),
                It.IsAny<Expression<Func<Genre,bool>>>()), Times.Once);
            mockUnitOfWork.Verify(x => x.Commit(), Times.Once);
        }
    }
}