using FlowerApp.Domain.ApplicationModels.FlowerModels;
using FlowerApp.Domain.Common;

namespace FlowerApp.Service.Services;

public interface IFlowersService
{
    Task<GetFlowerResponse> Get(Pagination pagination, FlowerFilterParams filterParams, FlowerSortOptions sortOptions, string? searchQuery);
    Task<Flower?> Get(int id);
}