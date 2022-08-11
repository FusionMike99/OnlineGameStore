using AutoMapper;
using OnlineGameStore.BLL.Models.General;
using OnlineGameStore.MVC.Models;

namespace OnlineGameStore.MVC.Mapper
{
    public class PublisherMappingProfile : Profile
    {
        public PublisherMappingProfile()
        {
            CreateMap<PublisherModel, EditPublisherViewModel>()
                .ReverseMap();

            CreateMap<PublisherModel, PublisherViewModel>();
        }
    }
}