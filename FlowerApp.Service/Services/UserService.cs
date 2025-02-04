using FlowerApp.Domain.Models.UserModels;
using FlowerApp.Service.Storages;

namespace FlowerApp.Service.Services;

public class UserService : IUserService
{
    private readonly IUserStorage userStorage;

    public UserService(IUserStorage userStorage)
    {
        this.userStorage = userStorage;
    }

    public async Task<User?> Get(string id)
    {
        var dbUser = await userStorage.Get(id);
        return dbUser;
    }

    public async Task<User?> Create(User user)
    {
        var result = await userStorage.Create(user);
        return result ? user : null;
    }

    public async Task<User?> Update(User user)
    {
        var isUpdated = await userStorage.Update(user);
        return isUpdated ? user : null;
    }
}