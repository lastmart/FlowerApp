using FlowerApp.Domain.Common;
using FlowerApp.Domain.DbModels;

namespace FlowerApp.Data.Storages;

public interface IFlowersStorage : IStorageBase<Flower, int>
{
    Flower? GetByName(string name);
    Flower? GetByScientificName(string scientificName);
    Task<SearchFlowersResult<Flower>> GetAll(Pagination pagination, string? sortByProperty = null);
    Task<IEnumerable<Flower>> FilterFlowers(FlowerFilter filter);
    Task<IEnumerable<Flower>> SortFlowers(FlowerSortOptions sortOptions);
}