using AutoMapper;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.MVC.Models;

namespace OnlineGameStore.MVC.Mapper
{
    public class PlatformTypeMappingProfile : Profile
    {
        public PlatformTypeMappingProfile()
        {
            CreateMap<PlatformType, EditPlatformTypeViewModel>()
                .ReverseMap();

            CreateMap<PlatformType, PlatformTypeViewModel>();
        }
    }
}