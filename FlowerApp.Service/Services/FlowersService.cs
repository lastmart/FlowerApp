using FlowerApp.Domain.Common;
using FlowerApp.Domain.Models.FlowerModels;
using FlowerApp.Service.Storages;

namespace FlowerApp.Service.Services;

public class FlowersService : IFlowersService
{
    private readonly IFlowersStorage flowersStorage;

    public FlowersService(IFlowersStorage flowersStorage)
    {
        this.flowersStorage = flowersStorage;
    }

    public async Task<GetFlowerResponse> GetBatch(Pagination pagination, FlowerFilterParams filterParams,
        FlowerSortOptions sortOptions)
    {
        var response = await flowersStorage.Get(pagination, filterParams, sortOptions);
        return response;
    }

    public async Task<Flower?> Get(int id)
    {
        var flower = await flowersStorage.Get(id);
        return flower;
    }

    public async Task<Flower?> GetByName(string name)
    {
        var flower = await flowersStorage.Get(name);
        return flower;
    }
}