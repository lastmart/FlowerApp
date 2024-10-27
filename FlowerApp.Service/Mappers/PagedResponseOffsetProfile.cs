using AutoMapper;
using FlowerApp.Domain.DbModels;
using FlowerApp.Domain.DTOModels;

namespace FlowerApp.Service.Mappers;

public class PagedResponseOffsetProfile : Profile
{
    public PagedResponseOffsetProfile()
    {
        CreateMap(typeof(PagedResponseOffset<>), typeof(PagedResponseOffsetDto<>));
    }
}