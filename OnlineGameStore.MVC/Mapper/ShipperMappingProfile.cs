using AutoMapper;
using OnlineGameStore.BLL.Models.General;
using OnlineGameStore.MVC.Models;

namespace OnlineGameStore.MVC.Mapper
{
    public class ShipperMappingProfile : Profile
    {
        public ShipperMappingProfile()
        {
            CreateMap<ShipperModel, ShipperViewModel>();
        }
    }
}