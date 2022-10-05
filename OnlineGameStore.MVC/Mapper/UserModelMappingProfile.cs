using AutoMapper;
using OnlineGameStore.BLL.Models.General;
using OnlineGameStore.Identity.Models;
using OnlineGameStore.MVC.Models;

namespace OnlineGameStore.MVC.Mapper
{
    public class UserModelMappingProfile : Profile
    {
        public UserModelMappingProfile()
        {
            CreateMap<LoginModel, LoginViewModel>()
                .ForMember(dest => dest.ReturnUrl, opts => opts.Ignore())
                .ReverseMap();
            
            CreateMap<RegisterModel, RegisterViewModel>()
                .ForMember(dest => dest.PasswordConfirm, opts => opts.Ignore())
                .ForMember(dest => dest.ReturnUrl, opts => opts.Ignore())
                .ReverseMap();
            
            CreateMap<UserModel, EditUserViewModel>()
                .ForMember(dest => dest.SelectedRole, opts => 
                    opts.MapFrom(source => source.Role))
                .ForMember(dest => dest.SelectedPublisher, opts => 
                    opts.MapFrom(source => source.PublisherId))
                .ForMember(dest => dest.Roles, opts => opts.Ignore())
                .ForMember(dest => dest.Publishers, opts => opts.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.Role, opts => 
                    opts.MapFrom(source => source.SelectedRole))
                .ForMember(dest => dest.PublisherId, opts => 
                    opts.MapFrom(source => source.SelectedPublisher));

            CreateMap<UserModel, UserViewModel>();
        }
    }
}