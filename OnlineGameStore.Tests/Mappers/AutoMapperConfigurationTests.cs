﻿using AutoMapper;
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