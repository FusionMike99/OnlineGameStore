using AutoMapper;
using OnlineGameStore.BLL.Models.General;
using OnlineGameStore.MVC.Models;

namespace OnlineGameStore.MVC.Mapper
{
    public class PlatformTypeMappingProfile : Profile
    {
        public PlatformTypeMappingProfile()
        {
            CreateMap<PlatformTypeModel, EditPlatformTypeViewModel>()
                .ReverseMap();

            CreateMap<PlatformTypeModel, PlatformTypeViewModel>();
        }
    }
}