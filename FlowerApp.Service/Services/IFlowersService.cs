using FlowerApp.Domain.Models.FlowerModels;
using FlowerApp.DTOs.Common;
using Flower = FlowerApp.DTOs.Common.Flowers.Flower;

namespace FlowerApp.Service.Services;

public interface IFlowersService
{
    Task<GetFlowerResponse> Get(
        Pagination pagination,
        FlowerFilterParams filterParams,
        FlowerSortOptions sortOptions
    );

    Task<List<Flower>> Get(string searchString);
}