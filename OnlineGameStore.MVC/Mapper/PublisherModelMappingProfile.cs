using AutoMapper;
using OnlineGameStore.BLL.Models.General;
using OnlineGameStore.MVC.Models;

namespace OnlineGameStore.MVC.Mapper
{
    public class PublisherModelMappingProfile : Profile
    {
        public PublisherModelMappingProfile()
        {
            CreateMap<PublisherModel, EditPublisherViewModel>()
                .ReverseMap();

            CreateMap<PublisherModel, PublisherViewModel>();
        }
    }
}