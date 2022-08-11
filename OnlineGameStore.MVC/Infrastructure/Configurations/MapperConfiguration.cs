using AutoMapper;
using OnlineGameStore.MVC.Mapper;

namespace OnlineGameStore.MVC.Infrastructure.Configurations
{
    public static class MapperConfiguration
    {
        public static void AddModelsProfiles(this IMapperConfigurationExpression expression)
        {
            var profiles = new[]
            {
                typeof(CommentMappingProfile),
                typeof(GameMappingProfile),
                typeof(GenreMappingProfile),
                typeof(PlatformTypeMappingProfile),
                typeof(PublisherMappingProfile),
                typeof(OrderMappingProfile),
                typeof(ShipperMappingProfile)
            };
            
            expression.AddMaps(profiles);
        }
    }
}