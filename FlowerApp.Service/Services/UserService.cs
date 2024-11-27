using AutoMapper;
using FlowerApp.Data.Storages;
using DbUser = FlowerApp.Domain.DbModels.User;
using ApplicationUser = FlowerApp.Domain.ApplicationModels.FlowerModels.User;

namespace FlowerApp.Service.Services;

public class UserService : IUserService
{
    private readonly IUserStorage userStorage;
    private readonly IMapper mapper;

    public UserService(IUserStorage userStorage, IMapper mapper)
    {
        this.userStorage = userStorage;
        this.mapper = mapper;
    }

    public async Task<DbUser?> Get(Guid id)
    {
        var dbUser = await userStorage.Get(id);
        return dbUser; 
    }

    public async Task<DbUser?> Create(ApplicationUser user)
    {
        var dbUser = mapper.Map<DbUser>(user);

        var result = await userStorage.Create(dbUser);

        return result ? dbUser : null;
    }

    public async Task<DbUser?> Update(Guid id, ApplicationUser user)
    {
        var existingUser = await userStorage.Get(id);
        if (existingUser == null) return null;
    
        mapper.Map(user, existingUser);
    
        var isUpdated = await userStorage.Update(id, existingUser);
        return isUpdated ? existingUser : null;
    }

}