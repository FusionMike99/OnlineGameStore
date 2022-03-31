using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
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
                .ForMember(dest => dest.Genres, source => source.MapFrom(game => game.GameGenres.Select(g => new SelectListItem
                {
                    Value = g.GenreId.ToString(),
                    Text = g.Genre.Name
                })))
                .ForMember(dest => dest.PlatformTypes, source => source.MapFrom(game => game.GamePlatformTypes.Select(p => new SelectListItem
                {
                    Value = p.PlatformId.ToString(),
                    Text = p.PlatformType.Type
                })));

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
                })));

            CreateMap<Game, GameViewModel>()
                .ReverseMap();
        }
    }
}
