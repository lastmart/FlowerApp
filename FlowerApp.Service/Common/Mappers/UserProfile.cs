using AutoMapper;
using ApplicationUser = FlowerApp.Domain.ApplicationModels.FlowerModels.User;
using DbUser = FlowerApp.Domain.DbModels.User;

namespace FlowerApp.Service.Common.Mappers;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<DbUser, ApplicationUser>();
        
        CreateMap<ApplicationUser, DbUser>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()); // Игнорируем Id, так как он генерируется в DbModel
    }
}