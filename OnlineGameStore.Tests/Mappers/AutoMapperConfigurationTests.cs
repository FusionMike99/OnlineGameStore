using System;
using AutoMapper;
using OnlineGameStore.BLL.Mapper;
using OnlineGameStore.MVC.Mapper;
using Xunit;

namespace OnlineGameStore.Tests.Mappers
{
    public class AutoMapperConfigurationTests
    {
        [Theory]
        [InlineData(typeof(GameModelMappingProfile))]
        [InlineData(typeof(CommentModelMappingProfile))]
        [InlineData(typeof(GenreModelMappingProfile))]
        [InlineData(typeof(OrderModelMappingProfile))]
        [InlineData(typeof(PlatformTypeModelMappingProfile))]
        [InlineData(typeof(PublisherModelMappingProfile))]
        [InlineData(typeof(ShipperModelMappingProfile))]
        [InlineData(typeof(EntitiesMappingProfiles))]
        public void MappingProfile_IsValid(Type type)
        {
            // Arrange
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(type));

            // Assert
            mapperConfiguration.AssertConfigurationIsValid();
        }
    }
}