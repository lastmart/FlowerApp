using AutoMapper;
using FlowerApp.Data.Storages;
using FlowerApp.Domain.Common;
using FlowerApp.Domain.DTOModels;

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

    public async Task<GetFlowerResponse> GetFlowers(Pagination pagination)
    {
        var response = await flowersStorage.GetAll(pagination);
        return mapper.Map<GetFlowerResponse>(response);
    }
}