using AutoMapper;
using FlowerApp.Domain.DbModels;
using FlowerApp.Domain.DTOModels;

namespace FlowerApp.Service.Mappers;

public class PageResponseProfile : Profile
{
    public PageResponseProfile()
    {
        CreateMap(typeof(SearchFlowersResult<Flower>), typeof(GetFlowerResponse));
    }
}