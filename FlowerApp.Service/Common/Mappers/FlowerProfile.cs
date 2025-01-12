using AutoMapper;
using FlowerApp.Data.DbModels.Flowers;
using ApplicationFlower = FlowerApp.Domain.Models.FlowerModels.Flower;
using DbFlower = FlowerApp.Data.DbModels.Flowers.Flower;
using DtoFlower = FlowerApp.DTOs.Common.Flowers.Flower;

namespace FlowerApp.Service.Common.Mappers;

public class FlowerProfile : Profile
{
    public FlowerProfile()
    {
        CreateMap<DbFlower, ApplicationFlower>()
            .ForMember(dest => dest.ToxicCategory, opt => opt.MapFrom(src =>
                src.ToxicCategory == ToxicCategory.Any
                    ? new List<string> { ToxicCategory.Any.ToString() }
                    : Enum.GetValues(typeof(ToxicCategory))
                        .Cast<ToxicCategory>()
                        .Where(tc => tc != ToxicCategory.Any && src.ToxicCategory.HasFlag(tc))
                        .Select(tc => tc.ToString())
                        .ToList()));

        CreateMap<ApplicationFlower, DtoFlower>();
    }
}