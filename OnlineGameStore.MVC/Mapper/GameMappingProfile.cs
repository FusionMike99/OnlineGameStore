using AutoMapper;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.MVC.Models;
using System.Linq;

namespace OnlineGameStore.MVC.Mapper
{
    public class GameMappingProfile : Profile
    {
        public GameMappingProfile()
        {
            CreateMap<Game, EditGameViewModel>()
                .ForMember(dest => dest.SelectedGenres, source => source.Ignore())
                .ForMember(dest => dest.SelectedPlatformTypes, source => source.Ignore());

            CreateMap<EditGameViewModel, Game>()
                .ForMember(dest => dest.GameGenres, source => source.MapFrom(game => game.SelectedGenres.Select(selected => new GameGenre
                {
                    GameId = game.Id,
                    GenreId = selected
                })))
                .ForMember(dest => dest.GamePlatformTypes, source => source.MapFrom(game => game.SelectedPlatformTypes.Select(selected => new GamePlatformType
                {
                    GameId = game.Id,
                    PlatformId = selected
                })))
                .ForMember(dest => dest.Comments, source => source.Ignore());

            CreateMap<Game, GameViewModel>()
                .ReverseMap();
        }
    }
}
