using FlowerApp.Domain.Models.AuthModels;
using FlowerApp.Domain.Models.UserModels;

namespace FlowerApp.Service.Storages;

public interface IUserStorage : IStorage<User, int>
{
    Task<User?> GetByGoogleId(string googleId);
}