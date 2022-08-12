﻿using AutoMapper;
using OnlineGameStore.BLL.Models.General;
using OnlineGameStore.BLL.Utils;
using OnlineGameStore.MVC.Models;

namespace OnlineGameStore.MVC.Mapper
{
    public class CommentModelMappingProfile : Profile
    {
        public CommentModelMappingProfile()
        {
            CreateMap<CommentModel, EditCommentViewModel>()
                .ReverseMap();

            CreateMap<CommentModel, CommentViewModel>()
                .ForMember(dest => dest.Body, source => source.MapFrom(comment => comment.IsDeleted ? Constants.DeletedCommentMessage : comment.Body))
                .ReverseMap();
        }
    }
}