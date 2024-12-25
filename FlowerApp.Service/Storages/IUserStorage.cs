using FlowerApp.Domain.Models.UserModels;

namespace FlowerApp.Service.Storages;

public interface IUserStorage : IStorage<User, int>
{
}