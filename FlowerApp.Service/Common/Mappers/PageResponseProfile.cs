using AutoMapper;
using FlowerApp.Domain.ApplicationModels.FlowerModels;
using FlowerApp.Domain.DbModels;
using Flower = FlowerApp.Domain.DbModels.Flower;

namespace FlowerApp.Service.Common.Mappers;

public class PageResponseProfile : Profile
{
    public PageResponseProfile()
    {
        CreateMap(typeof(SearchFlowersResult<Flower>), typeof(GetFlowerResponse));
    }
}