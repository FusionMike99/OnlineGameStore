﻿using System.Linq;
using AutoMapper;
using OnlineGameStore.DomainModels.Models;
using OnlineGameStore.DomainModels.Models.General;
using OnlineGameStore.MVC.Models;

namespace OnlineGameStore.MVC.Mapper
{
    public class OrderModelMappingProfile : Profile
    {
        public OrderModelMappingProfile()
        {
            CreateMap<OrderModel, OrderViewModel>()
                .ForMember(dest => dest.Total, source =>
                    source.MapFrom(order => order.OrderDetails.Sum(od => od.Price * od.Quantity * (decimal)(1 - od.Discount / 100))))
                .ForMember(dest => dest.OrderDetails, source => source.MapFrom(order => order.OrderDetails))
                .ForMember(dest => dest.OrderState,
                    opts => opts.MapFrom(order => order.OrderState))
                .ForMember(o => o.EnableModification,
                    opts => opts.Ignore());

            CreateMap<OrderDetailModel, OrderDetailViewModel>()
                .ForMember(dest => dest.ProductName, source => source.MapFrom(od => od.Product.Name))
                .ReverseMap()
                .ForMember(dest => dest.Product, source => source.Ignore());

            CreateMap<FilterOrderModel, FilterOrderViewModel>()
                .ReverseMap();

            CreateMap<OrderModel, ShipOrderModel>()
                .ReverseMap();
            
            CreateMap<OrderModel, EditOrderViewModel>()
                .ForMember(dest => dest.OrderId, source => source.MapFrom(od => od.Id))
                .ForMember(dest => dest.Shippers, source => source.Ignore())
                .ReverseMap();
            
            CreateMap<OrderDetailModel, EditOrderDetailViewModel>()
                .ReverseMap();
            
            CreateMap<ShipOrderModel, ShipOrderViewModel>()
                .ForMember(so => so.Shippers,
                    opts => opts.Ignore())
                .ReverseMap();
        }
    }
}