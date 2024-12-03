using AutoMapper;
using DbTrade = FlowerApp.Domain.DbModels.Trade; 
using ApplicationTrade = FlowerApp.Domain.ApplicationModels.TradeModels.Trade;

namespace FlowerApp.Service.Common.Mappers;

public class TradeProfile : Profile
{
    public TradeProfile()
    {
        CreateMap<DbTrade, ApplicationTrade>()
            .ForMember(dest => dest.UserIdentifier, opt => opt.MapFrom(src => src.UserIdentifier))
            .ForMember(dest => dest.FlowerName, opt => opt.MapFrom(src => src.FlowerName))
            .ForMember(dest => dest.PreferredTrade, opt => opt.MapFrom(src => src.PreferredTrade))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
            .ForMember(dest => dest.ExpiresAt, opt => opt.MapFrom(src => src.ExpiresAt))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
        
        CreateMap<ApplicationTrade, DbTrade>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.UserIdentifier, opt => opt.MapFrom(src => src.UserIdentifier))
            .ForMember(dest => dest.FlowerName, opt => opt.MapFrom(src => src.FlowerName))
            .ForMember(dest => dest.PreferredTrade, opt => opt.MapFrom(src => src.PreferredTrade))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
            .ForMember(dest => dest.ExpiresAt, opt => opt.MapFrom(src => src.ExpiresAt))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
    }
}