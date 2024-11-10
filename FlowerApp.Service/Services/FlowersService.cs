using AutoMapper;
using FlowerApp.Data.Storages;
using FlowerApp.Domain.ApplicationModels.FlowerModels;
using FlowerApp.Domain.Common;

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
        FlowerSortOptions sortOptions)
    {
        var response = await flowersStorage.Get(pagination, filterParams, sortOptions);
        return mapper.Map<GetFlowerResponse>(response);
    }

    public async Task<Flower?> Get(int id)
    {
        var flower = await flowersStorage.Get(id);
        return mapper.Map<Flower>(flower);
    }

    public async Task<Flower?> Get(string name)
    {
        var flower = await flowersStorage.Get(name);
        return mapper.Map<Flower>(flower);
    }
}