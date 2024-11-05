using FlowerApp.Domain.Common;
using FlowerApp.Domain.DTOModels;

namespace FlowerApp.Service.Services;

public interface IFlowersService
{
    Task<GetFlowerResponse> GetFlowers(Pagination pagination);
}