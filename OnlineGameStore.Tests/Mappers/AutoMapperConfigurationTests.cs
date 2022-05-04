using AutoMapper;
using OnlineGameStore.MVC.Mapper;
using System;
using Xunit;

namespace OnlineGameStore.Tests.Mappers
{
    public class AutoMapperConfigurationTests
    {
        [Theory]
        [InlineData(typeof(GameMappingProfile))]
        [InlineData(typeof(CommentMappingProfile))]
        [InlineData(typeof(GenreMappingProfile))]
        [InlineData(typeof(PlatformTypeMappingProfile))]
        [InlineData(typeof(PublisherMappingProfile))]
        [InlineData(typeof(OrderDetailMappingProfile))]
        public void MappingProfile_IsValid(Type type)
        {
            // Arrange
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(type);
            });

            // Assert
            mapperConfiguration.AssertConfigurationIsValid();
        }
    }
}
