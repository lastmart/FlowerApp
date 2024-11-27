using ApplicationUser = FlowerApp.Domain.ApplicationModels.FlowerModels.User;
using DbUser = FlowerApp.Domain.DbModels.User;

namespace FlowerApp.Service.Services;

public interface IUserService
{
    Task<DbUser?> Get(Guid id);
    Task<DbUser?> Create(ApplicationUser user);
    Task<DbUser?> Update(Guid id, ApplicationUser user);
}