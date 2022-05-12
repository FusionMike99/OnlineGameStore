﻿using System.Linq;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Community.AutoMapper;
using AutoFixture.Xunit2;
using OnlineGameStore.MVC.Mapper;

namespace OnlineGameStore.Tests.Helpers
{
    internal sealed class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute() : base(GetDefaultFixture)
        {
        }

        private static IFixture GetDefaultFixture()
        {
            var fixture = new Fixture().Customize(new CompositeCustomization(
                new AutoMoqCustomization(),
                new ControllerCustomization(),
                new ViewComponentCustomization(),
                new AutoMapperCustomization(cfg =>
                {
                    cfg.AddProfile(typeof(CommentMappingProfile));
                    cfg.AddProfile(typeof(GameMappingProfile));
                    cfg.AddProfile(typeof(GenreMappingProfile));
                    cfg.AddProfile(typeof(PlatformTypeMappingProfile));
                    cfg.AddProfile(typeof(PublisherMappingProfile));
                    cfg.AddProfile(typeof(OrderMappingProfile));
                })));

            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b =>
                fixture.Behaviors.Remove(b));

            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            return fixture;
        }
    }
}