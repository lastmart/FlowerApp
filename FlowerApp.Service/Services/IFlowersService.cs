using FlowerApp.Domain.Common;
using FlowerApp.Domain.DTOModels;

namespace FlowerApp.Service.Services;

public interface IFlowersService
{
    Task<FlowersPage<FlowerDto>> GetFlowers(Pagination pagination);
}