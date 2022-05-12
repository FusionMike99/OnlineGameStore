using System.Linq;
using AutoMapper;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.MVC.Models;

namespace OnlineGameStore.MVC.Mapper
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<Order, OrderViewModel>()
                .ForMember(dest => dest.Total, source =>
                    source.MapFrom(order => order.OrderDetails.Sum(od => od.Price * od.Quantity * (decimal)(1 - od.Discount))))
                .ForMember(dest => dest.OrderDetails, source => source.MapFrom(order => order.OrderDetails));

            CreateMap<OrderDetail, OrderDetailViewModel>()
                .ForMember(dest => dest.ProductName, source => source.MapFrom(od => od.Product.Name))
                .ReverseMap()
                .ForMember(dest => dest.Product, source => source.Ignore());
        }
    }
}