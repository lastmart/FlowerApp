using AutoMapper;
using ApplicationUser = FlowerApp.Domain.Models.UserModels.User;
using DbUser = FlowerApp.Data.DbModels.Users.User;

namespace FlowerApp.Service.Common.Mappers;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<DbUser, ApplicationUser>();

        CreateMap<ApplicationUser, DbUser>()
            .ForMember(dest => dest.GoogleId, opt => opt.Ignore());
        
        CreateMap<FlowerApp.DTOs.Common.Users.User, DbUser>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Telegram, opt => opt.MapFrom(src => src.Telegram));
        
        CreateMap<FlowerApp.DTOs.Common.Users.User, ApplicationUser>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Telegram, opt => opt.MapFrom(src => src.Telegram));
    }
}