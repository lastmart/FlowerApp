using AutoMapper;
using FlowerApp.Domain.DbModels;
using FlowerApp.Domain.DTOModels;

namespace FlowerApp.Service.Mappers;

public class FlowerProfile : Profile
{
    public FlowerProfile()
    {
        CreateMap<Flower, FlowerDto>()
            .ForMember(dest => dest.ToxicCategory, opt => opt.MapFrom(src => 
                src.ToxicCategory == ToxicCategory.None ? 
                    new List<string> { "None" } : 
                    Enum.GetValues(typeof(ToxicCategory))
                        .Cast<ToxicCategory>()
                        .Where(tc => tc != ToxicCategory.None && src.ToxicCategory.HasFlag(tc))
                        .Select(tc => tc.ToString())
                        .ToList()))
            .ForMember(dest => dest.LightParameters, opt => opt.MapFrom(src => new LightParametersDto
            {
                IlluminationInSuites = src.LightParameters.IlluminationInSuites,
                DurationInHours = src.LightParameters.DurationInHours
            }));
    }
}