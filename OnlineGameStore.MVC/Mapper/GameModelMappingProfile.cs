using System.Linq;
using AutoMapper;
using OnlineGameStore.DomainModels.Models.General;
using OnlineGameStore.MVC.Models;

namespace OnlineGameStore.MVC.Mapper
{
    public class GameModelMappingProfile : Profile
    {
        public GameModelMappingProfile()
        {
            CreateMap<GameModel, EditGameViewModel>()
                .ForMember(dest => dest.SelectedGenres,
                    source => source.MapFrom(game => game.GameGenres.Select(gg => gg.GenreId)))
                .ForMember(dest => dest.SelectedPlatformTypes,
                    source => source.MapFrom(game => game.GamePlatformTypes.Select(gg => gg.PlatformId)))
                .ForMember(dest => dest.SelectedPublisher, source => source.MapFrom(game => game.PublisherName))
                .ForMember(dest => dest.Genres, source => source.Ignore())
                .ForMember(dest => dest.PlatformTypes, source => source.Ignore())
                .ForMember(dest => dest.Publishers, source => source.Ignore());

            CreateMap<EditGameViewModel, GameModel>()
                .ForMember(dest => dest.GameGenres, source => source.MapFrom(game =>
                    game.SelectedGenres.Select(selected => new GameGenreModel
                    {
                        GameId = game.Id,
                        GenreId = selected
                    })))
                .ForMember(dest => dest.GamePlatformTypes, source => source.MapFrom(game =>
                    game.SelectedPlatformTypes.Select(selected => new GamePlatformTypeModel
                    {
                        GameId = game.Id,
                        PlatformId = selected
                    })))
                .ForMember(dest => dest.PublisherName, source => source.MapFrom(game => game.SelectedPublisher))
                .ForMember(dest => dest.Comments, source => source.Ignore())
                .ForMember(dest => dest.IsDeleted, source => source.Ignore())
                .ForMember(dest => dest.DeletedAt, source => source.Ignore())
                .ForMember(dest => dest.Publisher, source => source.Ignore())
                .ForMember(dest => dest.OrderDetails, source => source.Ignore());

            CreateMap<GameModel, GameViewModel>()
                .ForMember(dest => dest.Genres,
                    source => source.MapFrom(game => game.GameGenres.Select(g => g.Genre.Name)))
                .ForMember(dest => dest.PlatformTypes,
                    source => source.MapFrom(game => game.GamePlatformTypes.Select(g => g.PlatformType.Type)))
                .ForMember(dest => dest.Publisher, source => source.MapFrom(game => game.PublisherName));
        }
    }
}