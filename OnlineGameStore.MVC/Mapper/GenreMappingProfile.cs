using AutoMapper;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.MVC.Models;

namespace OnlineGameStore.MVC.Mapper
{
    public class GenreMappingProfile : Profile
    {
        public GenreMappingProfile()
        {
            CreateMap<Genre, EditGenreViewModel>()
                .ForMember(dest => dest.SelectedParentGenre, source => source.MapFrom(genre => genre.ParentId))
                .ForMember(dest => dest.Genres, source => source.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.ParentId, source => source.MapFrom(genre => genre.SelectedParentGenre));

            CreateMap<Genre, GenreViewModel>()
                .ForMember(dest => dest.ParentName, source => source.MapFrom(genre => genre.Parent.Name));
        }
    }
}