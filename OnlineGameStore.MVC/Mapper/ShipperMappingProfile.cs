using AutoMapper;
using OnlineGameStore.BLL.Entities.Northwind;
using OnlineGameStore.MVC.Models;

namespace OnlineGameStore.MVC.Mapper
{
    public class ShipperMappingProfile : Profile
    {
        public ShipperMappingProfile()
        {
            CreateMap<NorthwindShipper, ShipperViewModel>();
        }
    }
}