using AutoMapper;
using OnlineGameStore.BLL.Entities;
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
                .ForMember(dest => dest.ReplyToAuthor, source => source.MapFrom(comment => comment.ReplyTo.Name))
                .ReverseMap();
        }
    }
}
