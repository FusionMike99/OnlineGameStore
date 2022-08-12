using AutoMapper;
using OnlineGameStore.BLL.MappingProfiles;
using OnlineGameStore.MVC.Mapper;

namespace OnlineGameStore.MVC.Infrastructure.Configurations
{
    public static class MapperConfiguration
    {
        public static void AddModelsProfiles(this IMapperConfigurationExpression expression)
        {
            var profiles = new[]
            {
                typeof(CommentModelMappingProfile),
                typeof(GameModelMappingProfile),
                typeof(GenreModelMappingProfile),
                typeof(PlatformTypeModelMappingProfile),
                typeof(PublisherModelMappingProfile),
                typeof(OrderModelMappingProfile),
                typeof(ShipperModelMappingProfile)
            };
            
            expression.AddMaps(profiles);
        }

        public static void AddEntitiesProfiles(this IMapperConfigurationExpression expression)
        {
            var profiles = new[]
            {
                typeof(EntitiesMappingProfiles)
            };
            
            expression.AddMaps(profiles);
        }
    }
}