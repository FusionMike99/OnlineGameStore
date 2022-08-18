using AutoMapper;
using OnlineGameStore.DAL.Entities;
using OnlineGameStore.DAL.Entities.Northwind;
using OnlineGameStore.DomainModels;
using OnlineGameStore.DomainModels.Constants;
using OnlineGameStore.DomainModels.Enums;
using OnlineGameStore.DomainModels.Models.General;
using OnlineGameStore.ExtensionsUtility.Extensions;

namespace OnlineGameStore.DAL.Mapper
{
    public class EntitiesMappingProfiles : Profile
    {
        public EntitiesMappingProfiles()
        {
            #region Comment
            
            CreateMap<CommentEntity, CommentModel>()
                .ForMember(dest => dest.Game, opts => 
                    opts.MapFrom(source => source.Game))
                .ReverseMap()
                .ForMember(dest => dest.Game, opts => 
                    opts.MapFrom(source => source.Game));

            #endregion

            #region GameGenre
          
            CreateMap<GameGenreEntity, GameGenreModel>()
                .ReverseMap();

            #endregion

            #region Game

            CreateMap<GameEntity, GameModel>()
                .ForMember(dest => dest.DatabaseEntity, opts => 
                    opts.MapFrom(source => DatabaseEntity.GameStore))
                .ReverseMap();
            
            CreateMap<ProductEntity, GameModel>()
                .ForMember(dest => dest.Id, opts =>
                    opts.MapFrom(source => source.ProductId.ToGuid()))
                .ForMember(dest => dest.Description, opts => opts.Ignore())
                .ForMember(dest => dest.DateAdded, opts => 
                    opts.MapFrom(source => Constants.AddedAtDefault))
                .ForMember(dest => dest.DatePublished, opts => opts.Ignore())
                .ForMember(dest => dest.PublisherName, opts => opts.Ignore())
                .ForMember(dest => dest.Publisher, opts => opts.Ignore())
                .ForMember(dest => dest.Comments, opts => opts.Ignore())
                .ForMember(dest => dest.GameGenres, opts => opts.Ignore())
                .ForMember(dest => dest.GamePlatformTypes, opts => opts.Ignore())
                .ForMember(dest => dest.OrderDetails, opts => opts.Ignore())
                .ForMember(dest => dest.DatabaseEntity, opts =>
                    opts.MapFrom(source => DatabaseEntity.Northwind))
                .ForMember(dest => dest.IsDeleted, opts => opts.Ignore())
                .ForMember(dest => dest.DeletedAt, opts => opts.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.Id, opts =>
                    opts.Ignore())
                .ForMember(dest => dest.ProductId, opts =>
                    opts.MapFrom(source => source.Id.ToInt()));

            #endregion

            #region GamePlatformType

            CreateMap<GamePlatformTypeEntity, GamePlatformTypeModel>()
                .ReverseMap();

            #endregion

            #region Genre

            CreateMap<GenreEntity, GenreModel>()
                .ReverseMap();
            
            CreateMap<CategoryEntity, GenreModel>()
                .ForMember(dest => dest.Id, opts =>
                    opts.MapFrom(source => source.CategoryId.ToGuid()))
                .ForMember(dest => dest.ParentId, opts => opts.Ignore())
                .ForMember(dest => dest.Parent, opts => opts.Ignore())
                .ForMember(dest => dest.SubGenres, opts => opts.Ignore())
                .ForMember(dest => dest.GameGenres, opts => opts.Ignore())
                .ForMember(dest => dest.IsDeleted, opts => opts.Ignore())
                .ForMember(dest => dest.DeletedAt, opts => opts.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.Id, opts =>
                    opts.Ignore())
                .ForMember(dest => dest.CategoryId, opts =>
                    opts.MapFrom(source => source.Id.ToInt()));

            #endregion

            #region OrderDetail

            CreateMap<OrderDetail, OrderDetailModel>()
                .ReverseMap();
            
            CreateMap<OrderDetailMongoDbEntity, OrderDetailModel>()
                .ForMember(dest => dest.OrderId, opts =>
                    opts.MapFrom(source => source.OrderId.ToGuid()))
                .ForMember(dest => dest.Id, opts => opts.Ignore())
                .ForMember(dest => dest.GameKey, opts => opts.Ignore())
                .ForMember(dest => dest.Product, opts => opts.Ignore())
                .ForMember(dest => dest.Order, opts => opts.Ignore())
                .ForMember(dest => dest.IsDeleted, opts => opts.Ignore())
                .ForMember(dest => dest.DeletedAt, opts => opts.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.Id, opts =>
                    opts.Ignore())
                .ForMember(dest => dest.ProductId, opts =>
                    opts.Ignore())
                .ForMember(dest => dest.OrderId, opts =>
                    opts.MapFrom(source => source.OrderId.ToInt()));

            #endregion

            #region Order

            CreateMap<OrderEntity, OrderModel>()
                .ReverseMap();
            
            CreateMap<OrderMongoDbEntity, OrderModel>()
                .ForMember(dest => dest.Id, opts =>
                    opts.MapFrom(source => source.OrderId.ToGuid()))
                .ForMember(dest => dest.CustomerId, opts =>
                    opts.MapFrom(source => source.CustomerId.ToGuid()))
                .ForMember(dest => dest.CancelledDate, opts => opts.Ignore())
                .ForMember(dest => dest.OrderState, opts => 
                    opts.MapFrom(source => OrderState.Unknown))
                .ForMember(dest => dest.IsDeleted, opts => opts.Ignore())
                .ForMember(dest => dest.DeletedAt, opts => opts.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.Id, opts =>
                    opts.Ignore())
                .ForMember(dest => dest.OrderId, opts =>
                    opts.MapFrom(source => source.Id.ToInt()))
                .ForMember(dest => dest.CustomerId, opts =>
                    opts.MapFrom(source => source.CustomerId.ToString()));

            #endregion

            #region PlatformType

            CreateMap<PlatformTypeEntity, PlatformTypeModel>()
                .ReverseMap();

            #endregion

            #region Publisher

            CreateMap<PublisherEntity, PublisherModel>()
                .ForMember(dest => dest.DatabaseEntity, opts => 
                    opts.MapFrom(source => DatabaseEntity.GameStore))
                .ReverseMap();
            
            CreateMap<SupplierEntity, PublisherModel>()
                .ForMember(dest => dest.Id, opts =>
                    opts.MapFrom(source => source.SupplierId.ToGuid()))
                .ForMember(dest => dest.Description, opts => opts.Ignore())
                .ForMember(dest => dest.Games, opts => opts.Ignore())
                .ForMember(dest => dest.DatabaseEntity, opts =>
                    opts.MapFrom(source => DatabaseEntity.Northwind))
                .ForMember(dest => dest.IsDeleted, opts => opts.Ignore())
                .ForMember(dest => dest.DeletedAt, opts => opts.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.Id, opts =>
                    opts.Ignore())
                .ForMember(dest => dest.SupplierId, opts =>
                    opts.MapFrom(source => source.Id.ToInt()));

            #endregion

            #region Shipper

            CreateMap<ShipperEntity, ShipperModel>()
                .ForMember(dest => dest.Id, opts =>
                    opts.MapFrom(source => source.ShipperId.ToGuid()))
                .ForMember(dest => dest.IsDeleted, opts => opts.Ignore())
                .ForMember(dest => dest.DeletedAt, opts => opts.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.Id, opts =>
                    opts.Ignore())
                .ForMember(dest => dest.ShipperId, opts =>
                    opts.MapFrom(source => source.Id.ToInt()));

            #endregion
        }
    }
}