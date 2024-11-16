using AutoMapper;
using FlowerApp.Domain.DbModels;
using ApplicationFlower = FlowerApp.Domain.ApplicationModels.FlowerModels.Flower;
using DbFlower = FlowerApp.Domain.DbModels.Flower;

namespace FlowerApp.Service.Common.Mappers;

public class FlowerProfile : Profile
{
    public FlowerProfile()
    {
        CreateMap<DbFlower, ApplicationFlower>()
            .ForMember(dest => dest.ToxicCategory, opt => opt.MapFrom(src =>
                src.ToxicCategory == ToxicCategory.None
                    ? new List<string> { "None" }
                    : Enum.GetValues(typeof(ToxicCategory))
                        .Cast<ToxicCategory>()
                        .Where(tc => tc != ToxicCategory.None && src.ToxicCategory.HasFlag(tc))
                        .Select(tc => tc.ToString())
                        .ToList()));
    }
}