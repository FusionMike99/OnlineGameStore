using AutoMapper;
using OnlineGameStore.BLL.Entities;
using OnlineGameStore.MVC.Models;

namespace OnlineGameStore.MVC.Mapper
{
    public class PublisherMappingProfile : Profile
    {
        public PublisherMappingProfile()
        {
            CreateMap<Publisher, EditPublisherViewModel>()
                .ReverseMap();

            CreateMap<Publisher, PublisherViewModel>();
        }
    }
}
