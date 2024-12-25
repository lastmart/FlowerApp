using FlowerApp.Domain.Models.UserModels;

namespace FlowerApp.Service.Services;

public interface IUserService
{
    Task<User?> Get(Guid id);
    Task<User?> Create(User user);
    Task<User?> Update(Guid id, User user);
}