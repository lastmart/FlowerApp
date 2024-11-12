using FlowerApp.Domain.DbModels;

namespace FlowerApp.Data.Storages;

public interface IUserStorage: IStorage<User, Guid>
{
    Task<User> CreateUser(string name);
}