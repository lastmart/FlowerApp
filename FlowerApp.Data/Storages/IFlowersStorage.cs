using FlowerApp.Domain.ApplicationModels.FlowerModels;
using FlowerApp.Domain.Common;
using FlowerApp.Domain.DbModels;
using Flower = FlowerApp.Domain.DbModels.Flower;

namespace FlowerApp.Data.Storages;

public interface IFlowersStorage : IStorage<Flower, int>
{
    Task<SearchFlowersResult<Flower>> Get(
        Pagination pagination,
        FlowerFilterParams? filterParams = null,
        FlowerSortOptions? sortByProperty = null,
        string? searchSubstring = null
    );
}