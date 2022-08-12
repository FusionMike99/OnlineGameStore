using System.Linq;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Community.AutoMapper;
using AutoFixture.Xunit2;
using MongoDB.Bson;
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
                    cfg.AddProfile(typeof(CommentModelMappingProfile));
                    cfg.AddProfile(typeof(GameModelMappingProfile));
                    cfg.AddProfile(typeof(GenreModelMappingProfile));
                    cfg.AddProfile(typeof(PlatformTypeModelMappingProfile));
                    cfg.AddProfile(typeof(PublisherModelMappingProfile));
                    cfg.AddProfile(typeof(OrderModelMappingProfile));
                    cfg.AddProfile(typeof(ShipperModelMappingProfile));
                })));

            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b =>
                fixture.Behaviors.Remove(b));

            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            
            fixture.Register(ObjectId.GenerateNewId);

            return fixture;
        }
    }
}