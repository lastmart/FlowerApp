using FlowerApp.Domain.DbModels;

namespace FlowerApp.Data.Storages;

public interface IUserStorage
{
    Task<User?> Get(Guid id);
    Task<bool> Create(User user);
    Task<bool> Update(Guid id, User user);
}