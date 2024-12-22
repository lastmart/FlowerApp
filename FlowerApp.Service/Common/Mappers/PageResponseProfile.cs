using AutoMapper;
using FlowerApp.Data.DbModels.Flowers;
using FlowerApp.Domain.Models.FlowerModels;
using Flower = FlowerApp.Data.DbModels.Flowers.Flower;

namespace FlowerApp.Service.Common.Mappers;

public class PageResponseProfile : Profile
{
    public PageResponseProfile()
    {
        CreateMap(typeof(SearchFlowersResult<Flower>), typeof(GetFlowerResponse));
    }
}