// using FlowerApp.Domain.Models.UserModels;
// using FlowerApp.Service.Storages;
//
// namespace FlowerApp.Service.Services;
//
// public class UserService : IUserService
// {
//     private readonly IUserStorage userStorage;
//
//     public UserService(IUserStorage userStorage)
//     {
//         this.userStorage = userStorage;
//     }
//
//     public async Task<User?> Get(Guid id)
//     {
//         var dbUser = await userStorage.Get(id);
//         return dbUser;
//     }
//
//     public async Task<User?> Create(User user)
//     {
//         var result = await userStorage.Create(user);
//
//         return result ? user : null;
//     }
//
//     public async Task<User?> Update(Guid id, User user)
//     {
//         var existingUser = await userStorage.Get(id);
//         if (existingUser == null) return null;
//
//         var isUpdated = await userStorage.Update(id, user);
//         return isUpdated ? user : null;
//     }
// }