using AutoMapper;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Entities.Northwind;

namespace OnlineGameStore.BLL.Utils
{
    public class NorthwindMappingProfile : Profile
    {
        public NorthwindMappingProfile()
        {
            CreateMap<NorthwindProduct, Game>()
                .ForMember(g => g.Id,
                    opts => opts.MapFrom(p => p.ProductId))
                .ForMember(g => g.PublisherName,
                    opts => opts.MapFrom(p => p.Supplier.CompanyName))
                .ForMember(g => g.Description,
                    opts => opts.Ignore())
                .ForMember(g => g.DatePublished,
                    opts => opts.Ignore())
                .ForMember(g => g.Publisher,
                    opts => opts.Ignore())
                .ForMember(g => g.Comments,
                    opts => opts.Ignore())
                .ForMember(g => g.GameGenres,
                    opts => opts.Ignore())
                .ForMember(g => g.GamePlatformTypes,
                    opts => opts.Ignore())
                .ForMember(g => g.OrderDetails,
                    opts => opts.Ignore())
                .ForMember(g => g.IsDeleted,
                    opts => opts.Ignore())
                .ForMember(g => g.DeletedAt,
                    opts => opts.Ignore())
                .ForMember(g => g.DatabaseEntity,
                    opts => opts.Ignore())
                .ReverseMap()
                .ForMember(p => p.Id,
                    opts => opts.Ignore());
            
            CreateMap<NorthwindSupplier, Publisher>()
                .ForMember(p => p.Id,
                    opts => opts.MapFrom(s => s.SupplierId))
                .ForMember(p => p.Description,
                    opts => opts.Ignore())
                .ForMember(p => p.Games,
                    opts => opts.Ignore())
                .ForMember(p => p.IsDeleted,
                    opts => opts.Ignore())
                .ForMember(p => p.DeletedAt,
                    opts => opts.Ignore())
                .ForMember(p => p.DatabaseEntity,
                    opts => opts.Ignore())
                .ReverseMap()
                .ForMember(s => s.Id,
                    opts => opts.Ignore());
            
            CreateMap<NorthwindOrder, Order>()
                .ForMember(o => o.Id,
                    opts => opts.MapFrom(no => no.OrderId))
                .ForMember(o => o.CancelledDate,
                    opts => opts.MapFrom(no => no.RequiredDate))
                .ForMember(o => o.ShipVia,
                    opts => opts.MapFrom(no => no.Shipper.CompanyName))
                .ForMember(o => o.OrderState,
                    opts => opts.Ignore())
                .ForMember(o => o.OrderState,
                    opts => opts.Ignore())
                .ForMember(o => o.IsDeleted,
                    opts => opts.Ignore())
                .ForMember(o => o.DeletedAt,
                    opts => opts.Ignore());
            
            CreateMap<NorthwindOrderDetail, OrderDetail>()
                .ForMember(od => od.GameKey,
                    opts => opts.Ignore())
                .ForMember(od => od.Product,
                    opts => opts.Ignore())
                .ForMember(od => od.Order,
                    opts => opts.Ignore());
        }
    }
}