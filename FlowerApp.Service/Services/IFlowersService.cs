using FlowerApp.Domain.Common;
using FlowerApp.Domain.Models.FlowerModels;

namespace FlowerApp.Service.Services;

public interface IFlowersService
{
    Task<GetFlowerResponse> GetBatch(Pagination pagination, FlowerFilterParams filterParams,
        FlowerSortOptions sortOptions);

    Task<Flower?> Get(int id);
    Task<Flower?> GetByName(string name);
}