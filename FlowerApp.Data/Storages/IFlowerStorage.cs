using FlowerApp.Domain.DbModels;

namespace FlowerApp.Data.Storages;

public interface IFlowerStorage: IStorageBase<Flower, int>
{
    Flower? GetByName(string name);
    Flower? GetByScientificName(string scientificName);
    Task<PagedResponseOffset<Flower>> GetAll(int pageNumber, int pageSize);
    Task<IEnumerable<Flower>> FilterFlowers(FlowerFilter filter);
    Task<IEnumerable<Flower>> SortFlowers(FlowerSortOptions sortOptions);
}