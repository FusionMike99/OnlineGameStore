using AutoMapper;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.BLL.Utils;
using OnlineGameStore.MVC.Models;

namespace OnlineGameStore.MVC.Mapper
{
    public class CommentMappingProfile : Profile
    {
        public CommentMappingProfile()
        {
            CreateMap<Comment, EditCommentViewModel>()
                .ReverseMap();

            CreateMap<Comment, CommentViewModel>()
                .ForMember(dest => dest.Body, source => source.MapFrom(comment => comment.IsDeleted ? Constants.DeletedCommentMessage : comment.Body))
                .ReverseMap();
        }
    }
}