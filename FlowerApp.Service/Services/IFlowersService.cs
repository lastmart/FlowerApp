using FlowerApp.Domain.ApplicationModels.FlowerModels;
using FlowerApp.Domain.Common;

namespace FlowerApp.Service.Services;

public interface IFlowersService
{
    Task<GetFlowerResponse> Get(Pagination pagination, FlowerFilterParams filterParams, FlowerSortOptions sortOptions);
    Task<Flower?> Get(int id);
    Task<Flower?> Get(string name);
}