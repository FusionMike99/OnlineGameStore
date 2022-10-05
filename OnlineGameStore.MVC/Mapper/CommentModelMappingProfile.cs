using AutoMapper;
using OnlineGameStore.DomainModels;
using OnlineGameStore.DomainModels.Constants;
using OnlineGameStore.DomainModels.Models.General;
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