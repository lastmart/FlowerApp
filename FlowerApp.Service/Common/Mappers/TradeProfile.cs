using AutoMapper;
using DbTrade = FlowerApp.Data.DbModels.Trades.Trade;
using ApplicationTrade = FlowerApp.Domain.Models.TradeModels.Trade;
using DTOTrade = FlowerApp.DTOs.Common.Trades.Trade;

namespace FlowerApp.Service.Common.Mappers;

public class TradeProfile : Profile
{
    public TradeProfile()
    {
        CreateMap<DbTrade, ApplicationTrade>();
        
        CreateMap<ApplicationTrade, DbTrade>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<ApplicationTrade, DTOTrade>();
        
        CreateMap<DTOTrade, ApplicationTrade>();
    }
}