using AutoMapper;
using OnlineGameStore.BLL.Models.General;
using OnlineGameStore.MVC.Models;

namespace OnlineGameStore.MVC.Mapper
{
    public class PlatformTypeModelMappingProfile : Profile
    {
        public PlatformTypeModelMappingProfile()
        {
            CreateMap<PlatformTypeModel, EditPlatformTypeViewModel>()
                .ReverseMap();

            CreateMap<PlatformTypeModel, PlatformTypeViewModel>();
        }
    }
}