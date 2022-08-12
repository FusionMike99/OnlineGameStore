﻿using AutoMapper;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Entities.Northwind;
using OnlineGameStore.BLL.Enums;
using OnlineGameStore.BLL.Models.General;
using OnlineGameStore.BLL.Utils;

namespace OnlineGameStore.BLL.MappingProfiles
{
    public class EntitiesMappingProfiles : Profile
    {
        public EntitiesMappingProfiles()
        {
            #region Comment
            
            CreateMap<Comment, CommentModel>()
                .ForMember(dest => dest.Game, opts => 
                    opts.MapFrom(source => source.Game))
                .ReverseMap()
                .ForMember(dest => dest.Game, opts => 
                    opts.MapFrom(source => source.Game));

            #endregion

            #region GameGenre
          
            CreateMap<GameGenre, GameGenreModel>()
                .ReverseMap();

            #endregion

            #region Game

            CreateMap<Game, GameModel>()
                .ForMember(dest => dest.DatabaseEntity, opts => 
                    opts.MapFrom(source => DatabaseEntity.GameStore))
                .ReverseMap();
            
            CreateMap<NorthwindProduct, GameModel>()
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

            CreateMap<GamePlatformType, GamePlatformTypeModel>()
                .ReverseMap();

            #endregion

            #region Genre

            CreateMap<Genre, GenreModel>()
                .ReverseMap();
            
            CreateMap<NorthwindCategory, GenreModel>()
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
            
            CreateMap<NorthwindOrderDetail, OrderDetailModel>()
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

            CreateMap<Order, OrderModel>()
                .ReverseMap();
            
            CreateMap<NorthwindOrder, OrderModel>()
                .ForMember(dest => dest.Id, opts =>
                    opts.MapFrom(source => source.OrderId.ToGuid()))
                .ForMember(dest => dest.CancelledDate, opts => opts.Ignore())
                .ForMember(dest => dest.OrderState, opts => 
                    opts.MapFrom(source => OrderState.Unknown))
                .ForMember(dest => dest.IsDeleted, opts => opts.Ignore())
                .ForMember(dest => dest.DeletedAt, opts => opts.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.Id, opts =>
                    opts.Ignore())
                .ForMember(dest => dest.OrderId, opts =>
                    opts.MapFrom(source => source.Id.ToInt()));

            #endregion

            #region PlatformType

            CreateMap<PlatformType, PlatformTypeModel>()
                .ReverseMap();

            #endregion

            #region Publisher

            CreateMap<Publisher, PublisherModel>()
                .ForMember(dest => dest.DatabaseEntity, opts => 
                    opts.MapFrom(source => DatabaseEntity.GameStore))
                .ReverseMap();
            
            CreateMap<NorthwindSupplier, PublisherModel>()
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

            CreateMap<NorthwindShipper, ShipperModel>()
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