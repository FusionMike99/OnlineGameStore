using AutoMapper;
using OnlineGameStore.DomainModels.Models.General;
using OnlineGameStore.MVC.Models;

namespace OnlineGameStore.MVC.Mapper
{
    public class GenreModelMappingProfile : Profile
    {
        public GenreModelMappingProfile()
        {
            CreateMap<GenreModel, EditGenreViewModel>()
                .ForMember(dest => dest.SelectedParentGenre, source => source.MapFrom(genre => genre.ParentId))
                .ForMember(dest => dest.Genres, source => source.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.ParentId, source => source.MapFrom(genre => genre.SelectedParentGenre));

            CreateMap<GenreModel, GenreViewModel>()
                .ForMember(dest => dest.ParentName, source => source.MapFrom(genre => genre.Parent.Name));
        }
    }
}