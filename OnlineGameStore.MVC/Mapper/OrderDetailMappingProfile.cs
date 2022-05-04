using AutoMapper;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.MVC.Models;

namespace OnlineGameStore.MVC.Mapper
{
    public class OrderDetailMappingProfile : Profile
    {
        public OrderDetailMappingProfile()
        {
            CreateMap<OrderDetail, OrderDetailViewModel>()
                .ForMember(dest => dest.ProductName, source => source.MapFrom(od => od.Product.Name));
        }
    }
}
