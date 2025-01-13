using FlowerApp.Domain.Models.UserModels;

namespace FlowerApp.Service.Services;

public interface IUserService
{
    Task<User?> Get(string id);
    Task<User?> Create(User user);
    Task<User?> Update(string googleId, User user);
}