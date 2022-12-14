using AutoMapper;
using OnlineGameStore.DomainModels.Models.General;
using OnlineGameStore.MVC.Models;

namespace OnlineGameStore.MVC.Mapper
{
    public class ShipperModelMappingProfile : Profile
    {
        public ShipperModelMappingProfile()
        {
            CreateMap<ShipperModel, ShipperViewModel>();
        }
    }
}