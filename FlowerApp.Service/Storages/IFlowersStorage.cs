using FlowerApp.Domain.Models.FlowerModels;
using FlowerApp.DTOs.Common;
using Flower = FlowerApp.Domain.Models.FlowerModels.Flower;

namespace FlowerApp.Service.Storages;

public interface IFlowersStorage : IStorage<Flower, int>
{
    Task<GetFlowerResponse> Get(
        Pagination pagination,
        FlowerFilterParams? filterParams = null,
        FlowerSortOptions? sortByProperty = null
    );

    Task<List<Flower>> Get(string name);
}