using AutoMapper;
using FlowerApp.Data.Storages;
using FlowerApp.Domain.ApplicationModels.FlowerModels;
using FlowerApp.Domain.Common;
using System.Xml.Linq;

namespace FlowerApp.Service.Services;

public class FlowersService : IFlowersService
{
    private readonly IFlowersStorage flowersStorage;
    private readonly IMapper mapper;

    public FlowersService(IFlowersStorage flowersStorage, IMapper mapper)
    {
        this.flowersStorage = flowersStorage;
        this.mapper = mapper;
    }

    public async Task<GetFlowerResponse> Get(Pagination pagination, FlowerFilterParams filterParams,
        FlowerSortOptions sortOptions, string? searchQuery)
    {
        if (searchQuery is not null)
        {
            var flowers = new List<Flower>();
            var flower = await flowersStorage.Get(searchQuery);
            if (flower is not null)
            {
                flowers.Add(mapper.Map<Flower>(flower));
            }
            return new GetFlowerResponse(flowers.Count, flowers);
        }
        var response = await flowersStorage.Get(pagination, filterParams, sortOptions);
        return mapper.Map<GetFlowerResponse>(response);
    }

    public async Task<Flower?> Get(int id)
    {
        var flower = await flowersStorage.Get(id);
        return mapper.Map<Flower>(flower);
    }
}