using AutoMapper;
using FlowerApp.Domain.Models.FlowerModels;
using FlowerApp.DTOs.Common;

namespace FlowerApp.Service.Common.Mappers;

public class FlowerSortOptionProfile : Profile
{
    public FlowerSortOptionProfile()
    {
        CreateMap<RawSortOption, FlowerSortOptions>()
            .ForMember(dest => dest.SortOptions, opt => opt.MapFrom(src => ParseSortOptions(src))
            );
    }

    private static List<FlowerSortOption> ParseSortOptions(RawSortOption src)
    {
        var sortOptions = new List<FlowerSortOption>();

        if (string.IsNullOrEmpty(src.Sort))
            return sortOptions;

        foreach (var option in src.Sort.Split(","))
        {
            var direction = option.First();

            if (direction != '-' && direction != '+')
                throw new InvalidOperationException();

            
            if (!Enum.TryParse<FlowerSortField>(option.Skip(1).ToArray(), true, out var sortField))
                throw new InvalidOperationException();

            sortOptions.Add(new FlowerSortOption
            {
                FlowerSortField = sortField,
                IsDescending = direction == '-'
            });
        }

        return sortOptions;
    }
}