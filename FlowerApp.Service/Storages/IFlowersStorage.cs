using FlowerApp.Domain.Common;
using FlowerApp.Domain.Models.FlowerModels;

namespace FlowerApp.Service.Storages;

public interface IFlowersStorage : IStorage<Flower, int>
{
    Task<GetFlowerResponse> Get(
        Pagination pagination,
        FlowerFilterParams? filterParams = null,
        FlowerSortOptions? sortByProperty = null,
        string? searchSubstring = null
    );
}